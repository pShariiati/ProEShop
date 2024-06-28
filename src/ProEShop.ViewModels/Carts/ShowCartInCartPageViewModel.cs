namespace ProEShop.ViewModels.Carts;

/// <summary>
/// استفاده شده در صفحه تکی سبد خرید
/// ویو مدل نمایش محصولات داخل سبد خریدِ کاربری که صفحه سبد خرید را باز میکند
/// </summary>
public class ShowCartInCartPageViewModel
{
    public string ProductVariantProductPersianTitle { get; set; }

    public string ProductVariantGuaranteeFullTitle { get; set; }

    public string ProductVariantSellerShopName { get; set; }

    public bool IsDiscountActive { get; set; }

    public long ProductVariantId { get; set; }

    /// <summary>
    /// موجودی انبار برای این محصول
    /// اگه بیشتر از سه بود نیازی به مقدار دهی این پراپرتی نیست
    /// </summary>
    public byte ProductVariantCount { get; set; }

    public short ProductVariantMaxCountInCart { get; set; }

    public int ProductVariantPrice { get; set; }

    public int? ProductVariantOffPrice { get; set; }

    public string ProductVariantVariantColorCode { get; set; }

    public bool? ProductVariantVariantIsColor { get; set; }

    public string ProductVariantVariantValue { get; set; }

    /// <summary>
    /// تعداد محصولی که داخل سبد خرید است
    /// </summary>
    public short Count { get; set; }

    public string ProductPicture { get; set; }

    public int ProductVariantProductProductCode { get; set; }

    public string ProductVariantProductSlug { get; set; }

    public byte Score
    {
        get
        {
            var result = Math.Ceiling((ProductVariantPrice * Count) / (double)10000);
            if (result <= 1)
                return 1;
            if (result >= 150)
                return 150;
            return (byte)result;
        }
    }
}
