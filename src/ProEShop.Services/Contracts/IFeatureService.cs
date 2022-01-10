using ProEShop.Entities;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Features;

namespace ProEShop.Services.Contracts;

public interface IFeatureService : IGenericService<Feature>
{
    Task<ShowFeaturesViewModel> GetCategoryFeatures(ShowFeaturesViewModel model);
    Task<Feature> FindByTitleAsync(string title);
    Task<List<string>> AutocompleteSearch(string input);
}