using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
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

    /// <summary>
    /// استفاده شده در لایوت اصلی و دراپ داون سبد خرید
    /// گرفتن محصولاتی که این کاربر وارد سبد خرید خود کرده است ؟
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<ShowCartInDropDownViewModel>> GetCartsForDropDown(long userId);
}