using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities.Identity;

namespace SampleProject.DataLayer.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasOne(userRole => userRole.Role)
                .WithMany(userRole => userRole.UserRoles)
                .HasForeignKey(userRole => userRole.RoleId);

            builder.HasOne(userRole => userRole.User)
                .WithMany(userRole => userRole.UserRoles)
                .HasForeignKey(userRole => userRole.UserId);

            builder.ToTable("UserRoles");
        }
    }
}
