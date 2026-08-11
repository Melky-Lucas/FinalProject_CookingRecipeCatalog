using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Infrastructure.Mapping;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Test.Tools;
using Xunit;

namespace Test.Integration
{
    public class RecipeTests : IDisposable, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRecipeRepository _recipeRepository;
        private readonly RecipeCatalogDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObjectMapper _objectMapper;
        private readonly IApplicationValidator _validator;
        private readonly RecipeService _service;
        private readonly HttpClient _client;

        private readonly static CreateRecipeDTO createRecipeDTO = new()
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

        public RecipeTests(WebApplicationFactory<Program> factory)
        {
            _dbContext = TestTools.CreateInMemoryDbContext();
            
            _unitOfWork = new UnitOfWork(_dbContext);
            _recipeRepository = new RecipeRepository(_dbContext);

            var mapper = TestTools.GetMapper();

            var services = TestTools.services;
            services.AddTransient<IRecipeRepository>(_ => _recipeRepository);

            _serviceProvider = services.BuildServiceProvider();

            _objectMapper = new AutoMapperAdapter(mapper);
            _validator = new ApplicationValidator(_serviceProvider);

            _service = new RecipeService(_unitOfWork, _objectMapper, _validator);

            _client = factory.CreateClient();
        }


        [Fact]
        public async Task CreateRecipe_WithValidData_ShouldReturnSuccess()
        {
            // Arrange
            var newRecipe = createRecipeDTO;
            TestTools.SeedDatabase(_dbContext);

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
        public async Task GetRecipes_ShouldReturnOkAndList()
        {
            // Act
            var response = await _client.GetAsync("/api/recipe", CancellationToken.None);
            var recipes = await response.Content.ReadFromJsonAsync<IEnumerable<RecipeDTO>>(CancellationToken.None);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(recipes);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}