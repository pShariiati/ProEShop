using ProEShop.Entities;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Services.Contracts;

public interface ICategoryBrandService : IGenericService<CategoryBrand>
{
    /// <summary>
    /// آیا برند وارد شده در دسته بندی مورد نظر قرار دارد یا خیر ؟
    /// جهت استفاده در صحفه ایجاد محصول
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="brandId"></param>
    /// <returns></returns>
    Task<bool> CheckCategoryBrand(long categoryId, long brandId);

    /// <summary>
    /// گرفتن میزان درصد کمیسیون یک برند و دسته بندی
    /// جهت استفاده در صفحه ایجاد محصول
    /// هنگام عوض شدن سلکت باکس برند، این متود فراخوانی میشود
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="brandId"></param>
    /// <returns></returns>
    Task<(bool isSucessfull, byte value)> GetCommissionPercentage(long categoryId, long brandId);
}