using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbConfiguration
{
    public class IngredientCategoryConfig : IEntityTypeConfiguration<IngredientCategory>
    {
        public void Configure(EntityTypeBuilder<IngredientCategory> builder)
        {
            builder.Property(ic => ic.Name).HasMaxLength(100);

            builder.Property(ic => ic.Description).HasMaxLength(250);

            // Relationship
            builder.HasMany(ic => ic.Ingredients)
                   .WithOne(i => i.IngredientCategory)
                   .HasForeignKey(i => i.IngredientCategoryId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
        
    }
}
