using KidsRUs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsRUs.Persistence.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Property(x => x.FullName).HasMaxLength(50);
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.Property(x => x.PasswordHash).HasMaxLength(255);
        builder.Property(x => x.PasswordSalt).HasMaxLength(255);
        builder.Property(x => x.RefreshToken).HasMaxLength(100);
        builder.Property(x => x.RefreshTokenExpires).HasColumnType("datetime");
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Email)
            .HasConversion(e => e.ToLower(), e => e);
    }
}