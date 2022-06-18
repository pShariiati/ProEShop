using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.ProductVariants;

public class ShowProductVariantViewModel
{
    [Display(Name = "مقدار تنوع")]
    public string VariantValue { get; set; }

    public bool VariantIsColor { get; set; }

    public string VariantColorCode { get; set; }

    [Display(Name = "گارانتی")]
    public string GuaranteeTitle { get; set; }

    [Display(Name = "قیمت")]
    public int Price { get; set; }

    [Display(Name = "کد تنوع")]
    public int VariantCode { get; set; }
}