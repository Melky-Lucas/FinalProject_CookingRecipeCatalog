using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfiguration
{
    public class MeasureUnitConfig : IEntityTypeConfiguration<MeasureUnit>
    {
        public void Configure(EntityTypeBuilder<MeasureUnit> builder)
        {
            builder.HasIndex(mu => mu.Name).IsUnique();
            builder.HasIndex(mu => mu.Abbreviation).IsUnique();

            builder.Property(mu => mu.Name).HasMaxLength(100);

            builder.Property(mu => mu.Abbreviation).HasMaxLength(10);
        }
    }
}
