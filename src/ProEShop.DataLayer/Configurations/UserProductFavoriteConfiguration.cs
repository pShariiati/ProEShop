using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;

namespace ProEShop.DataLayer.Configurations;

public class UserProductFavoriteConfiguration : IEntityTypeConfiguration<UserProductFavorite>
{
    public void Configure(EntityTypeBuilder<UserProductFavorite> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ProductId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserProductsFavorites)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}