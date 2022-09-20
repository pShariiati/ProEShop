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
        #region Seller

        builder.HasMany(x => x.SellerProvinces)
            .WithOne(x => x.Province)
            .HasForeignKey(x => x.ProvinceId);

        builder.HasMany(x => x.SellerCities)
            .WithOne(x => x.City)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region Address

        builder.HasMany(x => x.AddressProvinces)
            .WithOne(x => x.Province)
            .HasForeignKey(x => x.ProvinceId);

        builder.HasMany(x => x.AddressCities)
            .WithOne(x => x.City)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}