using ProEShop.Entities;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Services.Contracts;

public interface IUsedDiscountCodeService : ICustomGenericService<UsedDiscountCode>
{
    /// <summary>
    /// چه تعداد از این کد تخفیف استفاده شده است ؟
    /// </summary>
    /// <param name="discountCodeId"></param>
    /// <returns></returns>
    Task<long> GetCountOfUsedDiscount(long discountCodeId);

    /// <summary>
    /// چه تعداد از این کد تخفیف توسط کاربرِ داخل سیستم، استفاده شده است ؟
    /// </summary>
    /// <param name="discountCodeId"></param>
    /// <returns></returns>
    Task<long> GetCountOfUsedDiscountByOneUser(long discountCodeId);
}