using ProEShop.Entities;
using ProEShop.ViewModels.DiscountCodes;

namespace ProEShop.Services.Contracts;

public interface IDiscountCodeService : IGenericService<DiscountCode>
{
    /// <summary>
    /// چک کردن کد تخفیف برای استفاده کردن
    /// </summary>
    /// <returns></returns>
    Task<CheckDiscountCodeViewModel> CheckForDiscountPrice(string code);
}