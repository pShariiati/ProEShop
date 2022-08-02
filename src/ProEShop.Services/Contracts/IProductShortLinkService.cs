using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.ProductShortLinks;

namespace ProEShop.Services.Contracts;

public interface IProductShortLinkService : IGenericService<ProductShortLink>
{
    Task<ShowProductShortLinksViewModel> GetProductShortLinks(ShowProductShortLinksViewModel model);

    /// <summary>
    /// گرفتن یک لینک کوتاه به صورت شانسی برای زمانیکه
    /// یک محصول ایجاد میکنیم که محصول را به این لینک کوتاه متصل کنیم
    /// </summary>
    /// <returns></returns>
    Task<ProductShortLink> GetProductShortLinkForCreateProduct();

    /// <summary>
    /// استفاده شده در صفحه مدیریت لینک ها در بخش ادمین
    /// گرفتن یک لینک جهت حذف کردن
    /// این لینک نباید به محصولی متصل باشد
    /// و وضعیت آن باید در حالت استفاده نشده باشد
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ProductShortLink> GetForDelete(long id);
}