using ProEShop.Entities;
using ProEShop.ViewModels.Brands;

namespace ProEShop.Services.Contracts;

public interface IBrandService : IGenericService<Brand>
{
    Task<ShowBrandsViewModel> GetBrands(ShowBrandsViewModel model);
}