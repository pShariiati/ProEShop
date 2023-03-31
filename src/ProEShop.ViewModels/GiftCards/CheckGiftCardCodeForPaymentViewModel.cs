namespace ProEShop.ViewModels.GiftCards;

/// <summary>
/// ویوو مدل نتیجه ی چک کردن کارت هدیه در صفحه پرداخت
/// و همچنین بخش پست، بخش پست یعنی زمانیکه فرم ارسال میشه
/// </summary>
public class CheckGiftCardCodeForPaymentViewModel
{
    public CheckGiftCardCodeForPaymentViewModel(bool result, int discountPrice, string message = null, long? giftCardId = null)
    {
        Result = result;
        DiscountPrice = discountPrice;
        Message = message;
        GiftCardId = giftCardId;
    }

    public bool Result { get; set; }

    /// <summary>
    /// میزان تخفیف کارت هدیه
    /// </summary>
    public int DiscountPrice { get; set; }

    public string Message { get; set; }

    public long? GiftCardId { get; }
}