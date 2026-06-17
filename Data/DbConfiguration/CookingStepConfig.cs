using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbConfiguration
{
    public class CookingStepConfig : IEntityTypeConfiguration<CookingStep>
    {
        public void Configure(EntityTypeBuilder<CookingStep> builder)
        {
            builder.Property(r => r.Title)
                .HasMaxLength(100);

            builder.Property(r => r.Description)
                .HasMaxLength(250);
        }
    }
}
