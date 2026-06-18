using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbConfiguration
{
    public class Recipe_IngredientConfig : IEntityTypeConfiguration<Recipe_Ingredient>
    {
        public void Configure(EntityTypeBuilder<Recipe_Ingredient> builder)
        {
            // Relationship
            builder.HasOne(ri => ri.Recipe)
                   .WithMany(r => r.Recipe_Ingredients)
                   .HasForeignKey(ri => ri.RecipeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ri => ri.Ingredient)
                   .WithMany(i => i.Recipe_Ingredients)
                   .HasForeignKey(ri => ri.IngredientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ri => ri.Unit)
                   .WithMany(u => u.Recipe_Ingredients)
                   .HasForeignKey(ri => ri.UnitId)
                   .OnDelete(DeleteBehavior.Restrict);
        }    
    }
}
