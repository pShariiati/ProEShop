using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;
using ProEShop.Entities.Identity;

namespace ProEShop.DataLayer.Configurations;

public class ReturnProductItemConfiguration : IEntityTypeConfiguration<ReturnProductItem>
{
    public void Configure(EntityTypeBuilder<ReturnProductItem> builder)
    {
        builder.HasOne(x => x.ReturnProduct)
            .WithMany(x => x.ReturnProductItems)
            .HasForeignKey(x => x.ReturnProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
