using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.Variants;

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

    /// <summary>
    /// گرفتن اطلاعات مربوط به محصول با آیدی مشخص
    /// جهت استفاده در صفحه افزودن تنوع محصول
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<AddVariantViewModel> GetProductInfoForAddVariant(long productId);

    /// <summary>
    /// گرفتن اطلاعات کامل یک محصول بر اساس آیدی
    /// جهت استفده در صفحه نمایش محصول
    /// </summary>
    /// <param name="productCode"></param>
    /// <returns></returns>
    Task<ShowProductInfoViewModel> GetProductInfo(long productCode);

    /// <summary>
    /// یافتن محصول بر اساس لینک کوتاه آن
    /// </summary>
    /// <returns></returns>
    Task<(int productCode, string slug)> FindByShortLink(string productShortLint);
}