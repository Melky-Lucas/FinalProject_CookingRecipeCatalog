using Application.Contract;
using Application.DTOs;
using Application.DTOs.Validators;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using FluentValidation;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Test.Tools;
using Xunit;

namespace Test.Units
{
    public class RecipeServiceTests : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Mock<IRecipeRepository> _recipeRepository;
        private readonly RecipeCatalogDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObjectMapper _objectMapper;
        private readonly IApplicationValidator _validator;
        private readonly RecipeService _service;

        private static CreateRecipeDTO createRecipeDTO = new()
        {
            Title = "Pasta Carbonara",
            Recipe_Ingredients = new List<CreateRecipe_IngredientDTO>
            {
                new () { IngredientId = 3, Quantity = 4, UnitId = 1, IsOptional = false }
            },
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

        private static CreateRecipeDTO invalidCreateRecipeDTO = new()
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
            _recipeRepository = new Mock<IRecipeRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Infrastructure.Mapping.MappingProfile>();
            }, new LoggerFactory());
            var mapper = config.CreateMapper();

            var services = new ServiceCollection();
            services.AddTransient<IValidator<CreateRecipeDTO>, CreateRecipeDTOValidator>();
            services.AddTransient<IValidator<CreateRecipe_IngredientDTO>, CreateRecipe_IngredientDTOValidator>();
            services.AddTransient<IRecipeRepository>(_ => _recipeRepository.Object);

            _serviceProvider = services.BuildServiceProvider();

            _objectMapper = new Infrastructure.Mapping.AutoMapperAdapter(mapper);
            _validator = new ApplicationValidator(_serviceProvider);

            _service = new RecipeService(_unitOfWork, _objectMapper, _validator);
        }


        [Fact]
        public async Task CreateRecipe_WithValidData_ShouldReturnSuccess()
        {
            // Arrange
            var newRecipe = createRecipeDTO;
            await TestTools.SeedDatabase(_dbContext);

            // Act
            var result = await _service.CreateAsync(newRecipe);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Pasta Carbonara", result.Data.Title);

            var recipeInDb = await _dbContext.Recipes.FirstOrDefaultAsync(r => r.Title == "Pasta Carbonara", TestContext.Current.CancellationToken);
            Assert.NotNull(recipeInDb);
        }

        [Fact]
        public async Task CreateRecipe_WithEmptyIngredients_ShouldReturnException()
        {
            // Arrange
            var invalidRecipe = invalidCreateRecipeDTO;
            await TestTools.SeedDatabase(_dbContext);
            _recipeRepository.Setup(r => r.HasTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _recipeRepository.Setup(r => r.HasImageURLAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            AppValidationException exception = await Assert.ThrowsAsync<AppValidationException>(async () => await _service.CreateAsync(invalidRecipe));

            // Assert
            Assert.Contains("One or more errors have occurred.", exception.Message);
            Assert.Contains("Recipe ingredients cannot be empty.", exception.Errors.SelectMany(e => e.Value).ToList());
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}