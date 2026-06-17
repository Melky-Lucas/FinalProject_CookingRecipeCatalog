using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbConfiguration
{
    public class Recipe_CategoryConfig : IEntityTypeConfiguration<Recipe_Category>
    {
        public void Configure(EntityTypeBuilder<Recipe_Category> builder)
        {
            builder.HasKey(RC => new { RC.RecipeId, RC.CategoryId });
        }
    }
}
