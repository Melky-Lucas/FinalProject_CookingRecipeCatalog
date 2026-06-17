using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace Data
{
    public class RecipeCatalogDBContext : DbContext
    {
        public RecipeCatalogDBContext(DbContextOptions<RecipeCatalogDBContext> options) : 
            base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe_Ingredient> Recipe_Ingredients { get; set; }
        public DbSet<Recipe_Category> Recipe_Categories { get; set; }
        public DbSet<CookingStep> CookingSteps { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<IngredientCategory> IngredientCategories { get; set; }
        public DbSet<MeasureUnit> MeasureUnits { get; set; }
    }
}
