using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Contracts;

public interface IProductService : IGenericService<Product>
{
    Task<ShowProductsViewModel> GetProducts(ShowProductsViewModel model);
    Task<List<string>> GetPersianTitlesForAutocomplete(string input);
    Task<ProductDetailsViewModel> GetProductDetails(long productId);

    /// <summary>
    /// حذف کردن محصولی که در وضعیت در انتظار تایید اولیه است
    /// در صفحه مدیریت محصولات از این متود استفاده میکنیم
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Product> GetProductToRemoveInManagingProducts(long id);
}