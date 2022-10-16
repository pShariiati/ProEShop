using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProEShop.Entities.Enums;

namespace ProEShop.ViewModels.Carts;

/// <summary>
/// ویوو مدل برای بخش ساخت سفارش
/// محصولات داخل سبد خرید کاربر رو میخونیم
/// </summary>
public class ShowCartForCreateOrderAndPayViewModel
{
    public long ProductVariantGuaranteeId { get; set; }

    public bool IsDiscountActive { get; set; }

    public long ProductVariantId { get; set; }

    public int ProductVariantPrice { get; set; }

    public int? ProductVariantOffPrice { get; set; }

    /// <summary>
    /// تعداد محصولی که داخل سبد خرید است
    /// </summary>
    public short Count { get; set; }

    public Dimension ProductVariantProductDimension { get; set; }

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
