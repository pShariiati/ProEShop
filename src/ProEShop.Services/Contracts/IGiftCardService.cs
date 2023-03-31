using ProEShop.Entities;
using ProEShop.ViewModels.GiftCards;

namespace ProEShop.Services.Contracts;

public interface IGiftCardService : IGenericService<GiftCard>
{
    /// <summary>
    /// چک کردن کارت هدیه برای استفاده کردن
    /// در صفحه پرداخت و بخش پست
    /// </summary>
    /// <param name="model"></param>
    /// <param name="showGiftCardId"></param>
    /// <returns></returns>
    Task<CheckGiftCardCodeForPaymentViewModel> CheckForGiftCardPriceForPayment(GetGiftCardCodeDataViewModel model, bool showGiftCardId);
}