using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;
using ProEShop.Entities.Identity;

namespace ProEShop.DataLayer.Configurations;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.HasMany(x => x.ConsignmentItems)
            .WithOne(x => x.ProductVariant)
            .HasForeignKey(x => x.ProductVariantId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}