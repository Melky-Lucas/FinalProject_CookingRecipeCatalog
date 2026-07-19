using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfiguration
{
    public class TipConfig : IEntityTypeConfiguration<Tip>
    {
        public void Configure(EntityTypeBuilder<Tip> builder)
        {
            builder.Property(t => t.Content)
                .HasMaxLength(250);
        }
    }
}
