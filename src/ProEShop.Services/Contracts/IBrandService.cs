using ProEShop.Entities;
using ProEShop.ViewModels.Brands;

namespace ProEShop.Services.Contracts;

public interface IBrandService : IGenericService<Brand>
{
    Task<ShowBrandsViewModel> GetBrands(ShowBrandsViewModel model);

    Task<EditBrandViewMode> GetForEdit(long id);

    Task<List<string>> AutocompleteSearch(string term);

    Task<List<long>> GetBrandIdsByList(List<string> brands);
    Task<Dictionary<long, string>> GetBrandsByCategoryId(long categoryId);
    Task<BrandDetailsViewModel> GetBrandDetails(long brandId);
}