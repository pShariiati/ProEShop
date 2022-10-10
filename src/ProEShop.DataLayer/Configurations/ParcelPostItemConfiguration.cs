using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;
using ProEShop.Entities.Identity;

namespace ProEShop.DataLayer.Configurations;

public class ParcelPostItemConfiguration : IEntityTypeConfiguration<ParcelPostItem>
{
    public void Configure(EntityTypeBuilder<ParcelPostItem> builder)
    {
        builder.HasKey(x => new { x.ParcelPostId, x.ProductVariantId });

        builder.HasOne(x => x.ProductVariant)
            .WithMany(x => x.ParcelPostItems)
            .HasForeignKey(x => x.ProductVariantId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
