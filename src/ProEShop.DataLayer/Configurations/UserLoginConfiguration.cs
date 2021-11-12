using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities.Identity;

namespace ProEShop.DataLayer.Configurations;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.HasOne(userLogin => userLogin.User)
            .WithMany(userLogin => userLogin.UserLogins)
            .HasForeignKey(userClaim => userClaim.UserId);

        builder.ToTable("UserLogins");
    }
}
