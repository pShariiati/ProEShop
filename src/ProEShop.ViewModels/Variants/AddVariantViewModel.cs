using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.Variants;

public class AddVariantViewModel
{
    [HiddenInput]
    public long ProductId { get; set; }

    public string Slug { get; set; }

    public int ProductCode { get; set; }

    [Display(Name = "تنوع")]
    [HiddenInput]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    public long VariantId { get; set; }

    [Display(Name = "گارانتی")]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    public long GuaranteeId { get; set; }

    [Display(Name = "قیمت")]
    [Range(1, int.MaxValue, ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    public int Price { get; set; }

    public string ProductTitle { get; set; }

    public string CategoryTitle { get; set; }

    public bool CategoryIsVariantColor { get; set; }

    public string BrandFullTitle { get; set; }

    public byte CommissionPercentage { get; set; }

    public string MainPicture { get; set; }

    public List<ShowCategoryVariantInAddVariantViewModel> Variants { get; set; }
}