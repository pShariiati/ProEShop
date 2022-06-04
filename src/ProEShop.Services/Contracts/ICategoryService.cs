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
}