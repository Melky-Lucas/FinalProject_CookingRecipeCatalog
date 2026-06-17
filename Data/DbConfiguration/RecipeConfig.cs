using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbConfiguration
{
    public class RecipeConfig : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.Property(r => r.Title)
                .HasMaxLength(100);

            builder.Property(r => r.Description)
                .HasMaxLength(250);

            builder.Property(r => r.ImageUrl)
                .HasMaxLength(250);

            builder.Property(r => r.Difficulty)
                .HasConversion<string>()
                .HasMaxLength(25);

            // Relationships
            builder.HasMany(r => r.CookingSteps)
                .WithOne(cs => cs.Recipe)
                .HasForeignKey(cs => cs.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Tips)
                .WithOne(t => t.Recipe)
                .HasForeignKey(t => t.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Recipe_Categories)
               .WithOne(rc => rc.Recipe)
               .HasForeignKey(rc => rc.RecipeId)
               .OnDelete(DeleteBehavior.Cascade);
        }    
    }
}
