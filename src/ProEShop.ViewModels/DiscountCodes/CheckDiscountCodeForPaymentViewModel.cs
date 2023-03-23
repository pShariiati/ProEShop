namespace ProEShop.ViewModels.DiscountCodes;

/// <summary>
/// ویوو مدل نتیجه ی چک کردن کد تخفیف در صفحه پرداخت
/// برای بخش پست
/// </summary>
public class CheckDiscountCodeForPaymentViewModel
{
    public CheckDiscountCodeForPaymentViewModel(bool result, int discountPrice, string message = null, long? discountCodeId = null)
    {
        Result = result;
        DiscountPrice = discountPrice;
        Message = message;
        DiscountCodeId = discountCodeId;
    }

    public bool Result { get; set; }

    /// <summary>
    /// میزان تخفیف
    /// </summary>
    public int DiscountPrice { get; set; }

    public string Message { get; set; }

    public long? DiscountCodeId { get; }
}