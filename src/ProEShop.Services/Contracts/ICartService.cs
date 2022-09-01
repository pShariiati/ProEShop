using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Contracts;

public interface ICartService : ICustomGenericService<Cart>
{
    /// <summary>
    /// استفاده شده در صفحه تکی محصول
    /// از داخل این تنوع ها که به این متد پاس داده شده
    /// کدام یک، برای این کاربر در داخل سبد خریدش اضافه شده ؟
    /// </summary>
    /// <param name="productVariantsIds"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<ProductVariantInCartForProductInfoViewModel>> GetProductVariantsInCart(List<long> productVariantsIds, long userId);
}