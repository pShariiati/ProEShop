using ProEShop.Entities;
using ProEShop.ViewModels.DiscountCodes;

namespace ProEShop.Services.Contracts;

public interface IDiscountCodeService : IGenericService<DiscountCode>
{
    /// <summary>
    /// چک کردن کد تخفیف برای استفاده کردن
    /// در صفحه پرداخت و بخش پست، بخش پست یعنی زمانی که فرم ارسال میشود
    /// </summary>
    /// <param name="model"></param>
    /// <param name="showDiscountCodeId"></param>
    /// <returns></returns>
    Task<CheckDiscountCodeForPaymentViewModel> CheckForDiscountPriceForPayment(GetDiscountCodeDataViewModel model, bool showDiscountCodeId);

    /// <summary>
    /// چک کردن صحت کد تخفیف بعد از بازگشت کاربر از درگاه
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    Task<(bool Result, string Message)> CheckForDiscountCodeInVerify(Order order);
}