using ProEShop.Entities;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.Search;
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

    /// <summary>
    /// استفاده شده در بخش افزایش موجودی محصولات در بخش انبارداری
    /// لیست تمامی محصولاتی که در داخل محموله وجود داره رو میگیریم تا بتونیم که
    /// وضعیت اونارو به "موجود" تغییر بدیم
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<List<Product>> GetProductsForChangeStatus(List<long> ids);

    /// <summary>
    /// گرفتن محصولات برای صفحه مقایسه محصولات
    /// </summary>
    /// <param name="productCodes"></param>
    /// <returns></returns>
    Task<List<ShowProductInCompareViewModel>> GetProductsForCompare(params int[] productCodes);

    /// <summary>
    /// گرفتن محصولات برای مودال افزودن محصول
    /// شامل صفحه بندی و جستجوی متنی
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="searchValue"></param>
    /// <param name="productCodesToHide">چه محصولاتی باید نمایش داده نشوند، برای مثال اگر
    /// در داخل صفحه مقایسه محصول اول اضافه شده نباید دیگر در داخل مودال افزودن محصول، محصول اول را نمایش دهیم</param>
    /// <returns></returns>
    Task<ShowProductInComparePartialViewModel> GetProductsForAddProductInCompare(int pageNumber, string searchValue, int[] productCodesToHide);

    /// <summary>
    /// گرفتن آیدی دسته بندی محصول
    /// </summary>
    /// <param name="productCode"></param>
    /// <returns></returns>
    Task<long> GetProductCategoryId(long productCode);

    /// <summary>
    /// صفحه بندی محصولات به صورت
    /// AJAX
    /// در صفحه جستجو
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    Task<ShowProductsInSearchOnCategoryViewModel> GetProductsByPaginationForSearch(SearchOnCategoryInputsViewModel inputs);
}