using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;
using ProEShop.Entities.Identity;

namespace ProEShop.DataLayer.Configurations;

public class ProvinceAndCityConfiguration : IEntityTypeConfiguration<ProvinceAndCity>
{
    public void Configure(EntityTypeBuilder<ProvinceAndCity> builder)
    {
        builder.HasMany(x => x.Provinces)
            .WithOne(x => x.Province)
            .HasForeignKey(x => x.ProvinceId);

        builder.HasMany(x => x.Cities)
            .WithOne(x => x.City)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}