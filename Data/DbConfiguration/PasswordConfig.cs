using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfiguration
{
    public class PasswordConfig : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> builder)
        {
            builder.Property(p => p.PasswordHash).HasMaxLength(255);

            // Relationships
            builder.HasOne(p => p.User)
                   .WithOne(u => u.Password)
                   .HasForeignKey<User>(u => u.PasswordId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
