using ProEShop.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.ProductVariants;

public class ShowProductVariantViewModel
{
    public long Id { get; set; }

    [Display(Name = "مقدار تنوع")]
    public string VariantValue { get; set; }

    public bool? VariantIsColor { get; set; }

    public string VariantColorCode { get; set; }

    [Display(Name = "گارانتی")]
    public string GuaranteeTitle { get; set; }

    [Display(Name = "قیمت")]
    public int Price { get; set; }

    [Display(Name = "قیمت با تخفیف")]
    public int? OffPrice { get; set; }

    [Display(Name = "درصد تخفیف")]
    public byte? OffPercentage { get; set; }

    [Display(Name = "تاریخ شروع تخفیف")]
    public string StartDateTime { get; set; }

    [Display(Name = "تاریخ پایان تخفیف")]
    public string EndDateTime { get; set; }

    [Display(Name = "کد تنوع")]
    public int VariantCode { get; set; }

    [Display(Name = "حداکثر تعداد در سبد خرید")]
    public short MaxCountInCart { get; set; }
}