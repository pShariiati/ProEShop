using Microsoft.VisualBasic.CompilerServices;
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

    /// <summary>
    /// گرفتن محصولات داخل سبد خرید این کاربر
    /// برای نمایش دادن در صفحه تکی سبد خرید
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<ShowCartInCartPageViewModel>> GetCartsForCartPage(long userId);

    /// <summary>
    /// گرفتن محصولات داخل سبد خرید این کاربر
    /// برای نمایش دادن در صفحه
    /// Checkout
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<ShowCartInCheckoutPageViewModel>> GetCartsForCheckoutPage(long userId);

    /// <summary>
    /// گرفتن محصولات داخل سبد خرید این کاربر
    /// برای نمایش دادن در صفحه
    /// Payment
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<ShowCartInPaymentPageViewModel>> GetCartsForPaymentPage(long userId);

    /// <summary>
    /// گرفتن محصولات داخل سبد خرید این کاربر
    /// برای بخش ایجاد سفارش
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<ShowCartForCreateOrderAndPayViewModel>> GetCartsForCreateOrderAndPay(long userId);

    /// <summary>
    /// استفاده شده در صفحه تکی سبد خرید جهت حذف تمامی آیتم های داخل سبد خرید
    /// گرفتن تمامی آیتم های داخل سبد خرید کاربر
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<Entities.Cart>> GetAllCartItems(long userId);

    /// <summary>
    /// در داخل آیتم های سبد خرید سرچ میکنیم که حداقل یک محصول با این برند آیدی وجود داشته باشد
    /// </summary>
    /// <param name="brandId"></param>
    /// <returns></returns>
    Task<bool> CheckBrandIdForExistingInCart(long brandId);

    /// <summary>
    /// در داخل آیتم های سبد خرید سرچ میکنیم که حداقل یک محصول با این آیدی دسته بندی وجود داشته باشد
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    Task<bool> CheckCategoryIdForExistingInCart(long categoryId);
}