using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.UnitOfWork;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Test.Units
{
    public class RecipeServiceTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly RecipeCatalogDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObjectMapper _objectMapper;
        private readonly Mock<IApplicationValidator> _Validator;
        private readonly RecipeService _service;

        private static CreateRecipeDTO createRecipeDTO = new CreateRecipeDTO
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

        public RecipeServiceTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<RecipeCatalogDBContext>()
                .UseSqlite(_connection)
                .Options;

            _dbContext = new RecipeCatalogDBContext(options);

            _dbContext.Database.EnsureCreated();

            _unitOfWork = new UnitOfWork(_dbContext);

            _Validator = new Mock<IApplicationValidator>();
            _Validator.Setup(v => v.ValidateAsync(It.IsAny<object>()));

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<Infrastructure.Mapping.MappingProfile>();
            }, new LoggerFactory());
            var mapper = config.CreateMapper();

            _objectMapper = new Infrastructure.Mapping.AutoMapperAdapter(mapper);

            _service = new RecipeService(_unitOfWork, _objectMapper, _Validator.Object);
        }


        [Fact]
        public async Task CreateRecipe_WithValidData_ShouldReturnSuccess()
        {
            // Arrange
            var newRecipe = createRecipeDTO;

            _dbContext.RecipeCategories.Add(new RecipeCategory { Id = 1, Name = "Pasta" });
            _dbContext.RecipeCategories.Add(new RecipeCategory { Id = 2, Name = "Italiana" });
            _dbContext.IngredientCategories.Add(new IngredientCategory { Id = 1, Name = "Meat", Description = "Delicious." });
            _dbContext.Ingredients.AddRange([
                new Ingredient { Id = 3, Name = "Pancetta", Description = "Cured pork belly", ImageUrl = "http://example.com/pancetta.jpg", IngredientCategoryId = 1 }, 
                new Ingredient { Id = 4, Name = "Parmesano", Description = "Aged Parmesan cheese", ImageUrl = "http://example.com/parmesano.jpg", IngredientCategoryId = 1 }, 
                new Ingredient { Id = 5, Name = "Uova", Description = "Fresh eggs", ImageUrl = "http://example.com/uova.jpg", IngredientCategoryId = 1 }, 
                new Ingredient { Id = 6, Name = "Pasta", Description = "Fresh pasta", ImageUrl = "http://example.com/pasta.jpg", IngredientCategoryId = 1 }]
            );
            _dbContext.MeasureUnits.Add(new MeasureUnit { Id = 1, Name = "Gramos", Abbreviation = "g" });
            _dbContext.Roles.Add(new Role { Id = 1, Name = "Chef" });
            _dbContext.Passwords.Add(new Password { Id = 1, PasswordHash = "hashedpassword" });
            _dbContext.Users.Add(new User { Id = 1, Username = "Chef Carlos", Email = "chef.carlos@example.com", PasswordId = 1, RoleId = 1 });
            await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken); 

            // Act
            var result = await _service.CreateAsync(newRecipe);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Pasta Carbonara", result.Data.Title);

            var recipeInDb = await _dbContext.Recipes.FirstOrDefaultAsync(r => r.Title == "Pasta Carbonara", TestContext.Current.CancellationToken);
            Assert.NotNull(recipeInDb);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _connection.Close();
            _connection.Dispose();
        }
    }
}