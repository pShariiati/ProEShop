using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
