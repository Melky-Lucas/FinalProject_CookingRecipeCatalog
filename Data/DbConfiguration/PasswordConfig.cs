using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbConfiguration
{
    public class PasswordConfig : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> builder)
        {
            builder.Property(p => p.PasswordHash).HasMaxLength(255);

            builder.Property(p => p.Salt).HasMaxLength(255);

            // Relationships
            builder.HasOne(p => p.User)
                   .WithOne(u => u.Password)
                   .HasForeignKey<Password>(p => p.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
