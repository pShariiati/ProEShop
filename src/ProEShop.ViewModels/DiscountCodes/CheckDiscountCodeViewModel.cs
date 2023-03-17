namespace ProEShop.ViewModels.DiscountCodes;

/// <summary>
/// ویوو مدل چک کردن کد تخفیف در صفحه پرداخت
/// </summary>
public class CheckDiscountCodeViewModel
{
    public CheckDiscountCodeViewModel(bool result, int discountPrice)
    {
        Result = result;
        DiscountPrice = discountPrice;
    }

    public bool Result { get; set; }

    /// <summary>
    /// میزان تخفیف
    /// </summary>
    public int DiscountPrice { get; set; }
}