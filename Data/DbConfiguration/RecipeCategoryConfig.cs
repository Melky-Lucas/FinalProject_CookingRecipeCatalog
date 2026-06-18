using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbConfiguration
{
    public class RecipeCategoryConfig : IEntityTypeConfiguration<RecipeCategory>
    {
        public void Configure(EntityTypeBuilder<RecipeCategory> builder)
        {
            builder.Property(rc  => rc.Name).HasMaxLength(100);

            builder.Property(rc => rc.Description).HasMaxLength(250);

            // Relationship
            builder.HasMany(rc => rc.Recipe_Categories)
                .WithOne(RC => RC.Category)
                .HasForeignKey(RC => RC.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
