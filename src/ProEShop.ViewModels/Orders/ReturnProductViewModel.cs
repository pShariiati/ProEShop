namespace ProEShop.ViewModels.Orders;

/// <summary>
/// نمایش محصولاتی که قابلیت مرجوعی دارند
/// </summary>
public class ReturnProductViewModel
{
    /// <summary>
    /// OrderId
    /// </summary>
    public long Id { get; set; }

    public long OrderNumber { get; set; }

    public List<ParcelPostItemInReturnProduct> ParcelPostItems { get; set; }
}

/// <summary>
/// محصولاتی که کاربر قراره یک یا چند تا از اونا رو مرجوع کنه
/// </summary>
public class ParcelPostItemInReturnProduct
{
    /// <summary>
    /// عنوان محصول
    /// </summary>
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

    /// <summary>
    /// آیدی تنوع
    /// </summary>
    public long ProductVariantId { get; set; }
}