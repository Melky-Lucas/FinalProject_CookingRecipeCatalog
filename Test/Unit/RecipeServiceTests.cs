using Application.Contract;
using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Mapping;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Test.Tools;
using Xunit;

namespace Test.Unit
{
    public class RecipeServiceTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Mock<IRecipeRepository> _mockRecipeRepository;
        private readonly RecipeCatalogDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObjectMapper _objectMapper;
        private readonly IApplicationValidator _validator;
        private readonly RecipeService _service;


        private readonly static CreateRecipeDTO invalidCreateRecipeDTO = new()
        {
            Title = "Pasta Carbonara",
            Recipe_Ingredients = new List<CreateRecipe_IngredientDTO>(),
            CookingSteps = new List<CreateRecipeStepDTO>
            {
                new () { StepNumber = 1, Instruction = "Boil pasta.", EstimatedDuration = TimeSpan.FromMinutes(10), Title = "Boil Pasta" },
                new () { StepNumber = 2, Instruction = "Cook pancetta.", EstimatedDuration = TimeSpan.FromMinutes(15), Title = "Cook Pancetta" },
                new () { StepNumber = 3, Instruction = "Mix eggs and cheese.", EstimatedDuration = TimeSpan.FromMinutes(5), Title = "Mix Ingredients" },
                new () { StepNumber = 4, Instruction = "Combine all ingredients.", EstimatedDuration = TimeSpan.FromMinutes(10), Title = "Combine Ingredients" }
            },
            Category_Ids = new int[] { 1, 2 },
            Calories = 500,
            CookingTime = TimeSpan.FromMinutes(30),
            Description = "A classic Italian pasta dish.",
            Difficulty = ModelEnums.RecipeDifficulty.Medium,
            ImageUrl = "http://example.com/image.jpg",
            IsPublic = true,
            PreparationTime = TimeSpan.FromMinutes(15),
            Servings = 4,
            UserId = 1,
            Tips = new List<CreateRecipeTipDTO>
            {
                new() { Content = "Use fresh ingredients for best taste.", UserId = 1 },
                new() { Content = "Serve immediately after cooking.", UserId = 1 }
            }
        };

        public RecipeServiceTests()
        {
            _dbContext = TestTools.CreateInMemoryDbContext();

            _unitOfWork = new UnitOfWork(_dbContext);

            _mockRecipeRepository = new Mock<IRecipeRepository>();

            var mapper = TestTools.GetMapper();

            var services = TestTools.services;
            services.AddTransient<IRecipeRepository>(_ => _mockRecipeRepository.Object);

            _serviceProvider = services.BuildServiceProvider();

            _objectMapper = new AutoMapperAdapter(mapper);
            _validator = new ApplicationValidator(_serviceProvider);

            _service = new RecipeService(_unitOfWork, _objectMapper, _validator);
        }

        [Fact]
        public async Task GetRecipeById_NotFound_ShouldReturnNotFoundResult()
        {
            // Arrange
            var notFoundId = 999;
            _mockRecipeRepository.Setup(r => r.GetByIdAsync(notFoundId))
                     .ReturnsAsync((Recipe?)null);

            // Act
            var result = await _service.GetByIdAsync(notFoundId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.StatusCode);
            Assert.Contains("Entity not found.", result.Message);
        }

        [Fact]
        public async Task CreateRecipe_WithEmptyIngredients_ShouldReturnException()
        {
            // Arrange
            var invalidRecipe = invalidCreateRecipeDTO;
            _mockRecipeRepository.Setup(r => r.HasTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _mockRecipeRepository.Setup(r => r.HasImageURLAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            AppValidationException exception = await Assert.ThrowsAsync<AppValidationException>(async () => await _service.CreateAsync(invalidRecipe));

            // Assert
            Assert.Contains("One or more errors have occurred.", exception.Message);
            Assert.Contains("Recipe ingredients cannot be empty.", exception.Errors.SelectMany(e => e.Value).ToList());
        }

    }
}
