using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.ProductVariants;

public class EditProductVariantViewModel
{
    [HiddenInput]
    public long Id { get; set; }

    public string Slug { get; set; }

    public int ProductCode { get; set; }

    [Display(Name = "قیمت")]
    [Range(1, 2000000000, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public int Price { get; set; }

    [Display(Name = "قیمت")]
    [Range(1, 2000000000, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public int? OffPrice { get; set; }

    [Display(Name = "قیمت")]
    [Range(1, 99, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public byte? OffPercentage { get; set; }

    [Display(Name = "تاریخ شروع تخفیف")]
    public string StartDateTime { get; set; }

    [Display(Name = "تاریخ پایان تخفیف")]
    public string EndDateTime { get; set; }

    public string ProductTitle { get; set; }

    public string CategoryTitle { get; set; }

    public bool CategoryIsVariantColor { get; set; }

    public string BrandFullTitle { get; set; }

    public byte CommissionPercentage { get; set; }

    public string MainPicture { get; set; }
}