using ProEShop.Entities;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Services.Contracts;

public interface ICategoryService : IGenericService<Category>
{
    Task<ShowCategoriesViewModel> GetCategories(ShowCategoriesViewModel model);
    Task<Dictionary<long, string>> GetCategoriesToShowInSelectBoxAsync(long? id = null);

    /// <summary>
    /// گرفتن دسته بندی هایی که زیر مجموعه ندارند
    /// جهت استفاده برای جستجو در صفحه مدیریت محصولات
    /// </summary>
    /// <returns></returns>
    Task<Dictionary<long, string>> GetCategoriesWithNoChild();
    Task<EditCategoryViewModel> GetForEdit(long id);
    Task<List<List<ShowCategoryForCreateProductViewModel>>> GetCategoriesForCreateProduct(long[] selectedCategoriesIds);
    Task<List<string>> GetCategoryBrands(long categoryId);
    Task<Category> GetCategoryWithItsBrands(long categoryId);
    Task<bool> CanAddFakeProduct(long categoryId);
    Task<(bool isSuccessful, List<long> categoryIds)> GetCategoryParentIds(long categoryId);

    /// <summary>
    /// فروشنده مورد نظر از چه دسته بندی هایی استفاده کرده است
    /// آنها را برگشت میزنیم
    /// جهت استفاده در صفحه مدیریت محصولات در بخش پنل فروشنده
    /// </summary>
    /// <returns></returns>
    Task<Dictionary<long, string>> GetSellerCategories();

    /// <summary>
    /// آیا تنوع این دسته بندی رنگ است یا اندازه یا کلا تنوع ندارد
    /// استفاده شده در صفحه ویرایش تنوع دسته بندی
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    Task<bool?> IsVariantTypeColor(long categoryId);

    /// <summary>
    /// خواندن یک دسته بندی
    /// تمامی رکورد های تنوع این دسته رو اینکلود میکنیم
    /// استفاده شده در صفحه ویرایش تنوع دسته بندی بخش ادمین
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    Task<Entities.Category> GetCategoryForEditVariant(long categoryId);

    /// <summary>
    /// بررسی کد محصولات در صفحه مقایسه
    /// باید دسته بندی تمامی محصولات وارد شده مشابه اولین محصول وارد شده باشد
    /// برای مثال اگر دسته بندی اولین محصول گوشی موبایل است
    /// سایر محصولات هم باید در دسته بندی گوشی موبایل باشند
    /// </summary>
    /// <param name="productCodes"></param>
    /// <returns></returns>
    Task<bool> CheckProductCategoryIdsInComparePage(params int[] productCodes);
}