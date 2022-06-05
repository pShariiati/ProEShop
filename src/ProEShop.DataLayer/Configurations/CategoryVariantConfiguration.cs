using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;
using ProEShop.Entities.Identity;

namespace ProEShop.DataLayer.Configurations;

public class CategoryVariantConfiguration : IEntityTypeConfiguration<CategoryVariant>
{
    public void Configure(EntityTypeBuilder<CategoryVariant> builder)
    {
        builder.HasKey(x => new { x.CategoryId, x.VariantId });
    }
}