using ProEShop.Entities;

namespace ProEShop.Services.Contracts;

public interface IReturnProductService : IGenericService<ReturnProduct>
{
    /// <summary>
    /// گرفتن شماره پیگیری برای درج رکورد جدید
    /// </summary>
    /// <returns></returns>
    Task<long> GetTrackingNumberForAddNewRecord();
}