using Microsoft.AspNetCore.Mvc;
using ProEShop.ViewModels.Variants;

namespace ProEShop.ViewModels.CategoryVariants;

/// <summary>
/// استفاده شده در صفحه ویرایش تنوع های دسته بندی
/// بخش ادمین
/// </summary>
public class EditCategoryVariantViewModel
{
    /// <summary>
    /// برای چه دسته بندی قرار است عملیات ویرایش تنوع صورت گیرد
    /// </summary>
    [HiddenInput]
    public long CategoryId { get; set; }

    /// <summary>
    /// لیست تمامی تنوع ها را به کاربر نشون میدیم
    /// که از بین این تنوع ها هر کدوم رو خواست به عنوان تنوع این دسته بندی
    /// اضافه کنیم
    /// </summary>
    public List<ShowVariantInEditCategoryVariantViewModel> Variants { get; set; }

    /// <summary>
    /// از قبل برای این دسته بندی چه تنوع هایی اضافه شده است
    /// باید در صفحه ویرایش تنوع دسته بندی به ادمین لیست تنوع هایی که از قبل برای این دسته بندی
    /// اضافه شده است رو نشون بدیم
    /// </summary>
    public List<long> SelectedVariants { get; set; }
        = new();
}