using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProEShop.Common.Constants;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;

namespace ProEShop.ViewModels.Categories;

public class EditCategoryViewModel
{
    [HiddenInput]
    public long Id { get; set; }

    [Display(Name = "تصویر انتخاب شده")]
    public string SelectedPicture { get; set; }

    [PageRemote(PageName = "Index", PageHandler = "CheckForTitleOnEdit",
        HttpMethod = "POST",
        ErrorMessage = AttributesErrorMessages.RemoteMessage,
        AdditionalFields = ViewModelConstants.AntiForgeryToken + "," + nameof(Id))]
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(100, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    public string Description { get; set; }

    [PageRemote(PageName = "Index", PageHandler = "CheckForSlugOnEdit",
        HttpMethod = "POST",
        ErrorMessage = AttributesErrorMessages.RemoteMessage,
        AdditionalFields = ViewModelConstants.AntiForgeryToken + "," + nameof(Id))]
    [Display(Name = "آدرس دسته بندی")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(130, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string Slug { get; set; }

    [Display(Name = "تصویر")]
    [MaxFileSize(2)]
    [IsImage]
    public IFormFile Picture { get; set; }

    [Display(Name = "والد")]
    public long? ParentId { get; set; }

    [Display(Name = "نمایش در منو های اصلی")]
    public bool ShowInMenus { get; set; }

    public List<SelectListItem> MainCategories { get; set; }

    [Display(Name = "آیا میتوان کالای غیر اصل اضافه کرد ؟")]
    public bool CanAddFakeProduct { get; set; }

    [Display(Name = "نوع تنوع")]
    public bool? IsVariantColor { get; set; }

    /// <summary>
    /// اگر در داخل جدول تنوع دسته بندی ها رکوردی برای این دسته بندی
    /// وجود داشته باشه، نباید اجازه بدیم نوع تنوع این دسته بندی تغییر پیدا کنه
    /// برای مثال قبلا تنوع رنگ رو برای گوشی موبایل اضافه کردیم و چند رنگ رو هم برای این دسته بندی اضافه کردیم
    /// و الان قصد داریم که نوع تنوع گوشی موبایل رو به سایز تغییر بدیم
    /// که این اشتباهه و نباید بذاریم که این ویرایش صورت گیرد
    /// </summary>
    public bool CanVariantTypeChange { get; set; }
}
