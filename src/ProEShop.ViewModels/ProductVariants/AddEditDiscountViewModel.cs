using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.ProductVariants;

public class AddEditDiscountViewModel
{
    [HiddenInput]
    public long Id { get; set; }

    public string Slug { get; set; }

    public int ProductCode { get; set; }
    
    [Display(Name = "قیمت")]
    public int Price { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "قیمت با تخفیف")]
    [Range(1, 2000000000, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    [DivisibleBy10]
    public int OffPrice { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "درصد تخفیف")]
    [Range(1, 99, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public byte OffPercentage { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "تاریخ شروع تخفیف")]
    public string StartDateTime { get; set; }

    public string StartDateTimeEn { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "تاریخ پایان تخفیف")]
    public string EndDateTime { get; set; }

    public string EndDateTimeEn { get; set; }

    public string ProductTitle { get; set; }

    public string ProductCategoryTitle { get; set; }

    public bool CategoryIsVariantColor { get; set; }

    public string ProductBrandFullTitle { get; set; }

    public byte CommissionPercentage { get; set; }

    public string MainPicture { get; set; }
}