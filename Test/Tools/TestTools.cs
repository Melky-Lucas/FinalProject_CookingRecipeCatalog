using Core.Models;
using Infrastructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.Tools
{
    public class TestTools : IDisposable
    {
        static SqliteConnection _connection = new("Filename=:memory:");

        public static async Task SeedDatabase(RecipeCatalogDBContext _dbContext)
        {
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

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
