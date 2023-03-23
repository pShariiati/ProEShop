using ProEShop.Entities;
using ProEShop.ViewModels.DiscountCodes;

namespace ProEShop.Services.Contracts;

public interface IDiscountCodeService : IGenericService<DiscountCode>
{
    /// <summary>
    /// چک کردن کد تخفیف برای استفاده کردن
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<CheckDiscountCodeViewModel> CheckForDiscountPrice(GetDiscountCodeDataViewModel model);

    /// <summary>
    /// چک کردن کد تخفیف برای استفاده کردن
    /// در صفحه پرداخت و بخش پست
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<CheckDiscountCodeForPaymentViewModel> CheckForDiscountPriceForPayment(GetDiscountCodeDataViewModel model);
}