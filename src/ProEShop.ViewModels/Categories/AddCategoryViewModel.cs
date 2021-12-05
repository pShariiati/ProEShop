using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProEShop.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.Categories;

public class AddCategoryViewModel
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(100, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    public string Description { get; set; }

    [Display(Name = "آدرس دسته بندی")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(130, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string Slug { get; set; }

    [Display(Name = "تصویر")]
    public IFormFile Picture { get; set; }

    [Display(Name = "والد")]
    public long? ParentId { get; set; }

    [Display(Name = "نمایش در منو های اصلی")]
    public bool ShowInMenus { get; set; }

    public SelectListItem MainCategories { get; set; }
}
