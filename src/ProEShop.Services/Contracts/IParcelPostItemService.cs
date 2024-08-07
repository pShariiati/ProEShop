﻿using ProEShop.Entities;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Contracts;

public interface IParcelPostItemService : ICustomGenericService<ParcelPostItem>
{
    /// <summary>
    /// محصولات بخش نظرات در بخش پروفایل کاربری
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ShowProductsInProfileCommentViewModel> GetProductsInProfileComment(ShowProductsInProfileCommentViewModel model);

    /// <summary>
    /// این متد از متد بالایی استفاده میکند، اینجا فقط بهش شماره صحفه پاس میدیم
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    Task<ShowProductsInProfileCommentViewModel> GetProductsInProfileComment(int pageNumber);

    /// <summary>
    /// آیا داخل محصولات سفارش کاربر، یک محصول با این برند وجود دارد یا خیر ؟
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="brandId"></param>
    /// <returns></returns>
    Task<bool> CheckBrandIdForExistingInOrder(long orderId, long brandId);

    /// <summary>
    /// آیا داخل محصولات سفارش کاربر، یک محصول با این دسته بندی وجود دارد یا خیر ؟
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    Task<bool> CheckCategoryIdForExistingInOrder(long orderId, long categoryId);

    /// <summary>
    /// بررسی محصولاتی که قرار است مرجوع شوند
    /// آیا به همان تعداد محصولی که توسط کلاینت به سمت سرور آمده است
    /// به همان تعداد محصول هم داخل آن سفارش، محصول وجود دارد
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productVariantIdsToReturn"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> CheckProductsVariantsForReturn(long orderId, List<long> productVariantIdsToReturn, long userId);
}