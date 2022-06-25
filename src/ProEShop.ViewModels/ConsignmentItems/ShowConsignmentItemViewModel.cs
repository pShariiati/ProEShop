using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.ConsignmentItems;

public class ShowConsignmentItemViewModel
{
    [Display(Name = "شناسه آیتم محموله")]
    public long Id { get; set; }

    public long ConsignmentId { get; set; }

    [Display(Name = "شناسه کالا")]
    public long ProductVariantProductId { get; set; }

    [Display(Name = "عنوان محصول")]
    public string ProductVariantProductPersianTitle { get; set; }

    [Display(Name = "مقدار تنوع")]
    public string ProductVariantVariantValue { get; set; }

    public string ProductVariantVariantColorCode { get; set; }

    public bool ProductVariantVariantIsColor { get; set; }

    [Display(Name = "قیمت")]
    public int ProductVariantPrice { get; set; }

    [Display(Name = "تعداد")]
    public int Count { get; set; }
}