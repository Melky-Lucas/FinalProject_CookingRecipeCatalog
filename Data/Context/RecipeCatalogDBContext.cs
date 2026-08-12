using Microsoft.EntityFrameworkCore;
using Core.Models;
using System.Reflection;

namespace Infrastructure.Context
{
    public class RecipeCatalogDBContext : DbContext
    {
        public RecipeCatalogDBContext(DbContextOptions<RecipeCatalogDBContext> options) : 
            base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Navigation(u => u.Password).AutoInclude();
            modelBuilder.Entity<User>().Navigation(u => u.Role).AutoInclude();

            modelBuilder.Entity<Ingredient>().Navigation(i => i.IngredientCategory).AutoInclude();

            modelBuilder.Entity<Recipe_Ingredient>().Navigation(ri => ri.Ingredient).AutoInclude();
            modelBuilder.Entity<Recipe_Ingredient>().Navigation(ri => ri.Unit).AutoInclude();

            modelBuilder.Entity<Recipe_Category>().Navigation(rc => rc.Category).AutoInclude();

            modelBuilder.Entity<Recipe>().Navigation(r => r.CookingSteps).AutoInclude();
            modelBuilder.Entity<Recipe>().Navigation(r => r.Tips).AutoInclude();
            modelBuilder.Entity<Recipe>().Navigation(r => r.Recipe_Categories).AutoInclude();
            modelBuilder.Entity<Recipe>().Navigation(r => r.Recipe_Ingredients).AutoInclude();
            modelBuilder.Entity<Recipe>().Navigation(r => r.User).AutoInclude();

            modelBuilder.Entity<Tip>().Navigation(t => t.User).AutoInclude();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
        public DbSet<Role> Roles { get; set; }
    }
}
