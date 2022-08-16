using ProEShop.Entities;

namespace ProEShop.Services.Contracts;

public interface ICategoryVariantService : ICustomGenericService<CategoryVariant>
{
    /// <summary>
    /// این دسته بندی چه تنوع هایی دارد ؟
    /// استفاده شده در صفحه ویرایش تنوع دسته بندی بخش ادمین
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    Task<List<long>> GetCategoryVariants(long categoryId);
}