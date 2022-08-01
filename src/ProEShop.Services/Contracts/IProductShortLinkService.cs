using ProEShop.Entities;
using ProEShop.ViewModels.Brands;

namespace ProEShop.Services.Contracts;

public interface IProductShortLinkService : IGenericService<ProductShortLink>
{
    /// <summary>
    /// گرفتن یک لینک کوتاه به صورت شانسی برای زمانیکه
    /// یک محصول ایجاد میکنیم که محصول را به این لینک کوتاه متصل کنیم
    /// </summary>
    /// <returns></returns>
    Task<ProductShortLink> GetProductShortLinkForCreateProduct();
}