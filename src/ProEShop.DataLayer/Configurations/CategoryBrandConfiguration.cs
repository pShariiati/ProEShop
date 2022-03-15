using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;
using ProEShop.Entities.Identity;

namespace ProEShop.DataLayer.Configurations;

public class CategoryBrandConfiguration : IEntityTypeConfiguration<CategoryBrand>
{
    public void Configure(EntityTypeBuilder<CategoryBrand> builder)
    {
        builder.HasKey(x => new { x.CategoryId, x.BrandId });
    }
}