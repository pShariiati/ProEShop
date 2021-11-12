using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities.Identity;

namespace ProEShop.DataLayer.Configurations;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.HasOne(roleClaim => roleClaim.Role)
            .WithMany(roleClaim => roleClaim.RoleClaims)
            .HasForeignKey(roleClaim => roleClaim.RoleId);

        builder.ToTable("RoleClaims");
    }
}
