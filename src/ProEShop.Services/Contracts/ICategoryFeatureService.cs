using ProEShop.Entities;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Services.Contracts;

public interface ICategoryFeatureService : IGenericService<Category>
{
    Task<ShowCategoryFeatureViewModel> GetCategoryFeatures(ShowCategoryFeaturesViewModel model);
}