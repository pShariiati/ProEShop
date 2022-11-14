using ProEShop.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.Orders;

/// <summary>
/// جزییات سفارش
/// </summary>
public class OrderDetailsViewModel
{
    public long Id { get; set; }

    public long OrderNumber { get; set; }

    public string AddressFullName { get; set; }

    public string AddressPhoneNumber { get; set; }

    public string AddressAddressLine { get; set; }

    public string CreatedDateTime { get; set; }

    public bool PayFromWallet { get; set; }

    public int TotalPrice { get; set; }

    public int? DiscountPrice { get; set; }

    public byte TotalScore { get; set; }

    public byte ShippingCount { get; set; }

    public List<ParcelPostForOrderDetailsViewModel> ParcelPosts { get; set; }
}

/// <summary>
/// مرسوله های جزییات سفارش
/// </summary>
public class ParcelPostForOrderDetailsViewModel
{
    public Dimension Dimension { get; set; }

    public ParcelPostStatus Status { get; set; }
    
    public string PostTrackingCode { get; set; }
    
    public int ShippingPrice { get; set; }

    public List<ParcelPostItemForOrderDetailsViewModel> ParcelPostItems { get; set; }
}

public class ParcelPostItemForOrderDetailsViewModel
{
    public string ProductVariantProductPersianTitle { get; set; }

    public string ProductVariantGuaranteeFullTitle { get; set; }

    public string ProductVariantSellerShopName { get; set; }

    public int Price { get; set; }

    public int? DiscountPrice { get; set; }

    public int Count { get; set; }

    public string ProductPicture { get; set; }

    public string ProductVariantProductProductCode { get; set; }

    public string ProductVariantProductSlug { get; set; }

    public string ProductVariantVariantColorCode { get; set; }

    public bool? ProductVariantVariantIsColor { get; set; }

    public string ProductVariantVariantValue { get; set; }
}