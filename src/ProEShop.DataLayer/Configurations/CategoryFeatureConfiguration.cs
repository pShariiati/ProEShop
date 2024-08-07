﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEShop.Entities;

namespace ProEShop.DataLayer.Configurations;

public class CategoryFeatureConfiguration : IEntityTypeConfiguration<CategoryFeature>
{
    public void Configure(EntityTypeBuilder<CategoryFeature> builder)
    {
        builder.HasKey(x => new { x.CategoryId, x.FeatureId });
    }
}
