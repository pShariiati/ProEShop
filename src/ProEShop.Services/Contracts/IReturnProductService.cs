using ProEShop.Entities;

namespace ProEShop.Services.Contracts;

public interface IReturnProductService : IGenericService<ReturnProduct>
{
    /// <summary>
    /// گرفتن شماره پیگیری برای درج رکورد جدید
    /// </summary>
    /// <returns></returns>
    Task<long> GetTrackingNumberForAddNewRecord();

	/// <summary>
    /// بررسی آیدی مرجوعی کالا هنگام نمایش دادن جزییات مرجوعی کالا
    /// </summary>
    /// <returns></returns>
    Task<bool> CheckForReturnProductDetails(long id);
}