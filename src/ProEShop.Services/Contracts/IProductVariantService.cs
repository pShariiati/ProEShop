﻿using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.ProductVariants;

namespace ProEShop.Services.Contracts;

public interface IProductVariantService : IGenericService<ProductVariant>
{
    Task<List<ShowProductVariantViewModel>> GetProductVariants(long productId);

    /// <summary>
    /// گرفتن آخرین کد تنوع محصول به علاوه یک
    /// جهت استفاده در صفحه افزودن تنوع محصول
    /// </summary>
    /// <returns></returns>
    Task<int> GetVariantCodeForCreateProductVariant();

    /// <summary>
    /// گرفتن اطلاعات مربوط به تنوع محصول
    /// جهت استفاده در صفحه ایجاد محموله
    /// </summary>
    /// <param name="variantCode"></param>
    /// <returns></returns>
    Task<ShowProductVariantInCreateConsignmentViewModel> GetProductVariantForCreateConsignment(int variantCode);

    /// <summary>
    /// گرفتن آیدی و کد تنوع در جدول تنوع های محصول برای بخش
    /// بک اند ساخت محموله
    /// </summary>
    /// <param name="variantCodes"></param>
    /// <returns></returns>
    Task<List<GetProductVariantInCreateConsignmentViewModel>> GetProductVariantsForCreateConsignment(List<int> variantCodes);

    /// <summary>
    /// گرفتن تنوع محصولات برای افزایش موجودی
    /// استفاده شده در صفحه ثبت نظر برای محموله
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<List<ProductVariant>> GetProductVariantsToAddCount(List<long> ids);

    /// <summary>
    /// استفاده شده در صفحه ویرایش تنوع محصول در بخش پنل فروشنده
    /// این متود دیتای داخل پارشل را پر میکند
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<EditProductVariantViewModel> GetDataForEdit(long id);

    /// <summary>
    /// استفاده شده در صفحه ویرایش تنوع محصول در بخش پنل فروشنده
    /// این تنوع باید برای فروشنده ایی باشد که در داخل سیستم لاگین است
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ProductVariant> GetForEdit(long id);
}