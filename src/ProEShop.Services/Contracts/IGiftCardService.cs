using ProEShop.Entities;
using ProEShop.ViewModels.GiftCards;

namespace ProEShop.Services.Contracts;

public interface IGiftCardService : IGenericService<GiftCard>
{
    /// <summary>
    /// چک کردن کارت هدیه برای استفاده کردن
    /// در صفحه پرداخت و بخش پست، بخش پست یعنی زمانی که فرم ارسال میشود
    /// </summary>
    /// <param name="model"></param>
    /// <param name="showGiftCardId"></param>
    /// <returns></returns>
    Task<CheckGiftCardCodeForPaymentViewModel> CheckForGiftCardPriceForPayment(GetGiftCardCodeDataViewModel model, bool showGiftCardId);

    /// <summary>
    /// چک کردن صحت کارت هدیه بعد از بازگشت کاربر از درگاه
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    Task<(bool Result, string Message)> CheckForGiftCardCodeInVerify(Order order);
}