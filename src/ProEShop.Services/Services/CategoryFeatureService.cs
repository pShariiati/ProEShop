using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class CategoryFeatureService : CustomGenericService<CategoryFeature>, ICategoryFeatureService
{
    private readonly DbSet<CategoryFeature> _categoryFeatures;
    public CategoryFeatureService(IUnitOfWork uow) : base(uow)
    {
        _categoryFeatures = uow.Set<CategoryFeature>();
    }
    public async Task<CategoryFeature> GetCategoryFeature(long categoryId, long featureId)
    {
        return await _categoryFeatures.Where(x => x.CategoryId == categoryId)
            .SingleOrDefaultAsync(x => x.FeatureId == featureId);
    }
}
