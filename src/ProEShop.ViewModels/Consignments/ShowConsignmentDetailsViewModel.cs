using System.ComponentModel.DataAnnotations;
using ProEShop.Entities;

namespace ProEShop.ViewModels.Consignments;

public class ShowConsignmentDetailsViewModel
{
    public long Id { get; set; }

    public string DeliveryDate { get; set; }

    public string SellerShopName { get; set; }

    public string Description { get; set; }

    public ConsignmentStatus ConsignmentStatus { get; set; }

    public List<ShowConsignmentItemViewModel> Items { get; set; }
}

public class ShowConsignmentItemViewModel
{
    [Display(Name = "شناسه آیتم محموله")]
    public long Id { get; set; }

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

    [Display(Name = "بارکد")]
    public string Barcode { get; set; }
}