using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.Products;

public class AddProductViewModel
{
    [Display(Name = "برند محصول")]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    public long BrandId { get; set; }

    [Display(Name = "اصالت کالا")]
    public bool IsFake { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "وزن")]
    public int Weight { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "طول")]
    public int Length { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "عرض")]
    public int Width { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "ارتفاع")]
    public int Height { get; set; }

    [Display(Name = "توضیحات کوتاه")]
    public string ShortDescription { get; set; }
}