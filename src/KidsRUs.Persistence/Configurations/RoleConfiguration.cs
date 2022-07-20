using KidsRUs.Domain.Common;
using KidsRUs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsRUs.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasData(
            new Role 
            {
                Id = (int)RoleType.Editor,
                Name = $"{RoleType.Editor}"
            },
            new Role
            {
                Id = (int)RoleType.Admin,
                Name = $"{RoleType.Admin}"
            });

        builder.HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);
    }
}