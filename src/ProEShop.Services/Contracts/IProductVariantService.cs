using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.ProductVariants;

namespace ProEShop.Services.Contracts;

public interface IProductVariantService : IGenericService<ProductVariant>
{
    Task<List<ShowProductVariantViewModel>> GetProductVariants(long productId);
}