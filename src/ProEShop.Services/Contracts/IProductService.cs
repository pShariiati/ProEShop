using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Contracts;

public interface IProductService : IGenericService<Product>
{
    Task<ShowProductsViewModel> GetProducts(ShowProductsViewModel model);
    Task<ShowProductsInSellerPanelViewModel> GetProductsInSellerPanel(ShowProductsInSellerPanelViewModel model);
    Task<ShowAllProductsInSellerPanelViewModel> GetAllProductsInSellerPanel(ShowAllProductsInSellerPanelViewModel model);
    Task<List<string>> GetPersianTitlesForAutocomplete(string input);
    Task<ProductDetailsViewModel> GetProductDetails(long productId);

    /// <summary>
    /// حذف کردن محصولی که در وضعیت در انتظار تایید اولیه است
    /// در صفحه مدیریت محصولات از این متود استفاده میکنیم
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Product> GetProductToRemoveInManagingProducts(long id);

    /// <summary>
    /// گرفتن آخرین کد محصول به علاوه یک
    /// جهت استفاده در صفحه افزودن محصول
    /// </summary>
    /// <returns></returns>
    Task<int> GetProductCodeForCreateProduct();

    /// <summary>
    /// جستجو محصولات بر اساس نام فارسی محصول
    /// استفاده شده در صفحه مدیریت محصولات پنل فروشنده
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<List<string>> GetPersianTitlesForAutocompleteInSellerPanel(string input);
}