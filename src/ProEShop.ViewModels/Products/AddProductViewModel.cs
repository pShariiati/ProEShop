using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;
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
    [Display(Name = "وزن بسته بندی")]
    [Range(1, 1000000, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public int PackWeight { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "طول بسته بندی")]
    [Range(1, 20000, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public int PackLength { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "عرض بسته بندی")]
    [Range(1, 20000, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public int PackWidth { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "ارتفاع بسته بندی")]
    [Range(1, 10000, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public int PackHeight { get; set; }

    [Display(Name = "توضیحات کوتاه")]
    public string ShortDescription { get; set; }

    [Display(Name = "بررسی تخصصی")]
    public string SpecialtyCheck { get; set; }

    [Display(Name = "تصاویر محصول")]
    [IsImage]
    public List<IFormFile> Pictures { get; set; }
}