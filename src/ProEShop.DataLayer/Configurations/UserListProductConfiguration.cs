using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;
using ProEShop.Entities.Identity;

namespace ProEShop.DataLayer.Configurations;

public class UserListProductConfiguration : IEntityTypeConfiguration<UserListProduct>
{
    public void Configure(EntityTypeBuilder<UserListProduct> builder)
    {
        builder.HasKey(x => new { x.UserListId, x.ProductId });

        builder.HasOne(x => x.UserList)
            .WithMany(x => x.UserListsProducts)
            .HasForeignKey(x => x.UserListId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
