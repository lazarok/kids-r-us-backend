using KidsRUs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsRUs.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");
        builder.Property(x => x.FileName).HasMaxLength(255);
        builder.HasIndex(x => new {x.ProductId, x.FileName}).IsUnique();

        builder.HasOne(i => i.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(p => p.ProductId);
    }
}