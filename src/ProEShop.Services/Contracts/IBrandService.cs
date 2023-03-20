using ProEShop.Entities;
using ProEShop.ViewModels.Brands;

namespace ProEShop.Services.Contracts;

public interface IBrandService : IGenericService<Brand>
{
    Task<ShowBrandsViewModel> GetBrands(ShowBrandsViewModel model);

    Task<EditBrandViewMode> GetForEdit(long id);

    Task<List<string>> AutocompleteSearch(string term);

    Task<Dictionary<long, string>> GetBrandsByFullTitle(List<string> brandTitles);
    Task<Dictionary<long, string>> GetBrandsByCategoryId(long categoryId);
    Task<BrandDetailsViewModel> GetBrandDetails(long brandId);
    Task<Brand> GetInActiveBrand(long brandId);

    /// <summary>
    /// گرفتن عنوان برند
    /// </summary>
    /// <param name="brandId"></param>
    /// <returns></returns>
    Task<string> GetBrandTitle(long brandId);
}