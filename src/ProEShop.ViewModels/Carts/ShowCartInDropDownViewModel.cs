using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.ViewModels.Carts;

/// <summary>
/// استفاده شده در لایوت اصلی و دراپ داون سبد خرید
/// ویوو مدل نمایش محصولاتی که کاربر جاری وارد سبد خرید خود کرده است
/// </summary>
public class ShowCartInDropDownViewModel
{
    public string ProductVariantProductPersianTitle { get; set; }

    public bool IsDiscountActive { get; set; }

    public long ProductVariantId { get; set; }

    public int ProductVariantCount { get; set; }

    public short ProductVariantMaxCountInCart { get; set; }

    public int ProductVariantPrice { get; set; }

    public int? ProductVariantOffPrice { get; set; }

    public string ProductVariantVariantColorCode { get; set; }

    public string ProductVariantVariantValue { get; set; }

    public short Count { get; set; }

    public string ProductPicture { get; set; }
}
