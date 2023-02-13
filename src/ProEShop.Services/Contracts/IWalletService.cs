using ProEShop.Entities;

namespace ProEShop.Services.Contracts;

public interface IWalletService : IGenericService<Wallet>
{
    /// <summary>
    /// گرفتن کیف پول بر اساس کد پیگیری
    /// </summary>
    /// <param name="trackingNumber"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<Entities.Wallet> FindByTrackingNumber(long trackingNumber, long userId);
}