using KidsRUs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsRUs.Persistence.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.HasIndex(x => new {x.ProductId, x.Name}).IsUnique();

        builder.HasOne(t => t.Product)
            .WithMany(p => p.Tags)
            .HasForeignKey(p => p.ProductId);
    }
}