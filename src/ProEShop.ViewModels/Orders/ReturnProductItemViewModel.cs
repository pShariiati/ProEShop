namespace ProEShop.ViewModels.Orders;

/// <summary>
/// ویو مدل جزییات مرجوعی کالا
/// </summary>
public class ReturnProductItemViewModel
{
    /// <summary>
    /// عنوان محصول
    /// </summary>
    public string PersianTitle { get; set; }

    /// <summary>`
    /// عکس محصول
    /// </summary>
    public string ProductImage { get; set; }

    /// <summary>
    /// نام فروشگاه
    /// </summary>
    public string ShopName { get; set; }

    /// <summary>
    /// عنوان گارانتی محصول
    /// </summary>
    public string GuaranteeTitle { get; set; }

    /// <summary>
    /// مقدار تنوع این محصول
    /// </summary>
    public string VariantValue { get; set; }

    /// <summary>
    /// آیا تنوع این محصول رنگ است
    /// </summary>
    public bool IsVariantColor { get; set; }

    /// <summary>
    /// اگر تنوع این محصول رنگ بود
    /// کد رنگ محصول چه مقداری دارد
    /// </summary>
    public string VariantColorCode { get; set; }
}