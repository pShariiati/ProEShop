using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;

namespace ProEShop.DataLayer.Configurations;

public class DiscountNoticeConfiguration : IEntityTypeConfiguration<DiscountNotice>
{
    public void Configure(EntityTypeBuilder<DiscountNotice> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ProductId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.DiscountNotices)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
