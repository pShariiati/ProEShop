using ProEShop.Entities;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Services.Contracts;

public interface ICategoryFeatureService : ICustomGenericService<CategoryFeature>
{
    Task<CategoryFeature> GetCategoryFeature(long categoryId, long featureId);
    Task<List<CategoryFeatureForCreateProductViewModel>> GetCategoryFeatures(long categoryId);
    Task<bool> CheckCategoryFeature(long categoryId, long featureId);
    Task<bool> CheckCategoryFeaturesCount(long categoryId, List<long> featureIds);
}
