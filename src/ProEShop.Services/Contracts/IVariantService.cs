using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Variants;

namespace ProEShop.Services.Contracts;

public interface IVariantService : IGenericService<Variant>
{
    Task<ShowVariantsViewModel> GetVariants(ShowVariantsViewModel model);
}