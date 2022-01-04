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
    private readonly DbSet<CategoryFeature> _categories;
    public CategoryFeatureService(IUnitOfWork uow)
        : base(uow)
    {
        _categories = uow.Set<CategoryFeature>();
    }

    public Task<ShowCategoryFeatureViewModel> GetCategoryFeatures(ShowCategoryFeaturesViewModel model)
    {
        throw new NotImplementedException();
    }
}
