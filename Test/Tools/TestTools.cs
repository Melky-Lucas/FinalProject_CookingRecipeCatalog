using Application.Contract;
using Application.DTOs;
using Application.DTOs.Validators;
using AutoMapper;
using Core.Models;
using FluentValidation;
using Infrastructure.Context;
using Infrastructure.PasswordHasher;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebAPI.Configuration;

namespace Test.Tools
{
    public class TestTools : IDisposable
    {
        public static IServiceCollection services = new ServiceCollection()
            .AddTransient<IValidator<CreateRecipeDTO>, CreateRecipeDTOValidator>()
            .AddTransient<IValidator<CreateRecipe_IngredientDTO>, CreateRecipe_IngredientDTOValidator>();

        private static SqliteConnection _connection = new("Data Source=:memory:");

        public static SqliteConnection GetConnection()
        {
            _connection.Open();
            return _connection;
        }

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Infrastructure.Mapping.MappingProfile>();
            }, new LoggerFactory());

            return config.CreateMapper();
        }

        public static RecipeCatalogDBContext CreateInMemoryDbContext()
        {
            _connection.Open();

            var options = new DbContextOptionsBuilder<RecipeCatalogDBContext>()
                .UseSqlite(_connection)
                .Options;

            RecipeCatalogDBContext _dbContext = new(options);

            _dbContext.Database.EnsureCreated();

            return _dbContext;
        }

        public static void SeedDatabase(RecipeCatalogDBContext _dbContext)
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _dbContext.RecipeCategories.Add(new RecipeCategory { Name = "Pasta" });
            _dbContext.RecipeCategories.Add(new RecipeCategory { Name = "Italiana" });

            _dbContext.IngredientCategories.Add(new IngredientCategory { Name = "Meat", Description = "Delicious." });

            _dbContext.Ingredients.AddRange([
                new Ingredient { Name = "Pancetta", Description = "Cured pork belly", ImageUrl = "http://example.com/pancetta.jpg", IngredientCategoryId = 1 },
                new Ingredient { Name = "Parmesano", Description = "Aged Parmesan cheese", ImageUrl = "http://example.com/parmesano.jpg", IngredientCategoryId = 1 },
                new Ingredient { Name = "Uova", Description = "Fresh eggs", ImageUrl = "http://example.com/uova.jpg", IngredientCategoryId = 1 },
                new Ingredient { Name = "Pasta", Description = "Fresh pasta", ImageUrl = "http://example.com/pasta.jpg", IngredientCategoryId = 1 }]
            );

            _dbContext.MeasureUnits.Add(new MeasureUnit { Name = "Gramos", Abbreviation = "g" });

            _dbContext.Roles.Add(new Role { Name = "Chef" });

            _dbContext.Passwords.Add(new Password { PasswordHash = new PasswordHasherAdapter().Hash("hashedpassword") });

            _dbContext.Users.Add(new User { Username = "Chef Carlos", Email = "chef.carlos@example.com", PasswordId = 1, RoleId = 1 });

            _dbContext.SaveChanges();
        }
        public static async Task<string> GetTestAdminTokenAsync(IAuthService authService)
        {
            LoginDTO loginDTO = new("chef.carlos@example.com", "hashedpassword");
            var authResponse = await authService.LoginAsync(loginDTO);
            string token = authResponse.Token;

            return token;
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
