namespace ProEShop.ViewModels.Orders;

/// <summary>
/// نمایش محصولاتی که قابلیت مرجوعی دارند
/// </summary>
public class ReturnProductViewModel
{
    public long OrderNumber { get; set; }

    public List<ParcelPostItemInReturnProduct> ParcelPostItems { get; set; }
}

public class ParcelPostItemInReturnProduct
{
    public string ProductVariantProductPersianTitle { get; set; }

    public int Price { get; set; }

    public string ProductPicture { get; set; }

    /// <summary>
    /// نام فروشگاه
    /// </summary>
    public string ProductVariantSellerShopName { get; set; }

    public string GuaranteeTitle { get; set; }

    /// <summary>
    /// کد محصول
    /// </summary>
    public string ProductVariantProductProductCode { get; set; }

    /// <summary>
    /// اسلاگ
    /// </summary>
    public string ProductVariantProductSlug { get; set; }

    /// <summary>
    /// آیا تنوع داره
    /// </summary>
    public bool? ProductVariantVariantIsColor { get; set; }

    /// <summary>
    /// رنگ تنوع
    /// </summary>
    public string ProductVariantVariantColorCode { get; set; }

    /// <summary>
    /// مقدار تنوع
    /// </summary>
    public string ProductVariantVariantValue { get; set; }
}