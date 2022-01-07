using ProEShop.Entities;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Features;

namespace ProEShop.Services.Contracts;

public interface ICategoryFeatureService : ICustomGenericService<CategoryFeature>
{
    Task<CategoryFeature> GetCategoryFeatureToRemove(long categoryId, long featureId);
}