using ProEShop.Entities.Enums;

namespace ProEShop.ViewModels.Carts;

/// <summary>
/// ویوو مدل صفحه
/// Checkout
/// </summary>
public class CheckoutViewModel
{
    /// <summary>
    /// آدرس کاربر
    /// </summary>
    public AddressInCheckoutPageViewModel UserAddress { get; set; }

    /// <summary>
    /// آیتم های داخل سبد خرید کاربر
    /// </summary>
    public List<ShowCartInCheckoutPageViewModel> CartItems { get; set; }
}

public class AddressInCheckoutPageViewModel
{
    public string FullName { get; set; }

    public string AddressLine { get; set; }

    public string ProvinceTitle { get; set; }

    public string CityTitle { get; set; }
}

public class ShowCartInCheckoutPageViewModel
{
    public bool IsDiscountActive { get; set; }

    /// <summary>
    /// موجودی انبار برای این محصول
    /// اگه بیشتر از سه بود نیازی به مقدار دهی این پراپرتی نیست
    /// </summary>
    public byte ProductVariantCount { get; set; }

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