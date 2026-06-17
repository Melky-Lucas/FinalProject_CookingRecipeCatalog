using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbConfiguration
{
    public class MeasureUnitConfig : IEntityTypeConfiguration<MeasureUnit>
    {
        public void Configure(EntityTypeBuilder<MeasureUnit> builder)
        {
            builder.Property(mu => mu.Name).HasMaxLength(100);

            builder.Property(mu => mu.Abbreviation).HasMaxLength(10);
        }
    }
}
