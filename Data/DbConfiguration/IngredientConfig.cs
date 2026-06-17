using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbConfiguration
{
    public class IngredientConfig : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        { 
            builder.Property(i => i.Name).HasMaxLength(100);

            builder.Property(i => i.Description).HasMaxLength(250);

            builder.Property(i => i.ImageUrl).HasMaxLength(250);
        }
    }
}
