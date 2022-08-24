using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.ProductVariants;

public class EditProductVariantViewModel
{
    [HiddenInput]
    public long Id { get; set; }

    public string Slug { get; set; }

    public int ProductCode { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Display(Name = "قیمت")]
    [Range(1, 2000000000, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    [DivisibleBy10]
    public int Price { get; set; }

    public bool IsDiscountActive { get; set; }

    public string ProductTitle { get; set; }

    public string ProductCategoryTitle { get; set; }

    public bool? CategoryIsVariantColor { get; set; }

    public string ProductBrandFullTitle { get; set; }

    public byte CommissionPercentage { get; set; }

    public string MainPicture { get; set; }
}