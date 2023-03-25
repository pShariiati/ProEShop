using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities;

namespace ProEShop.DataLayer.Configurations;

public class UsedDiscountCodeConfiguration : IEntityTypeConfiguration<UsedDiscountCode>
{
    public void Configure(EntityTypeBuilder<UsedDiscountCode> builder)
    {
        builder.HasKey(x => new { x.UserId, x.DiscountCodeId, x.OrderId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.UsedDiscountCodes)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}