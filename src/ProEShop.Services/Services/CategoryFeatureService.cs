using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Services.Services;

public class CategoryFeatureService : GenericService<Category>, ICategoryFeatureService
{
    private readonly DbSet<CategoryFeature> _categoryFeatures;
    public CategoryFeatureService(IUnitOfWork uow)
        : base(uow)
    {
        _categoryFeatures = uow.Set<CategoryFeature>();
    }

    public async Task<ShowCategoryFeaturesViewModel> GetCategoryFeatures(ShowCategoryFeaturesViewModel model)
    {
        var categoryFeatures = _categoryFeatures.AsQueryable();
        
        var paginationResult = await GenericPaginationAsync(categoryFeatures, model.Pagination);

        return new()
        {
            CategoryFeatures = await paginationResult.Query
            .Select(x => new ShowCategoryFeatureViewModel
            {
                Title = x.Feature.Title
            })
            .ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }
}
