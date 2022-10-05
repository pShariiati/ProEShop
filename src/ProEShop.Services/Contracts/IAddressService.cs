using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;

namespace ProEShop.Services.Contracts;

public interface IAddressService : IGenericService<Address>
{
    /// <summary>
    /// گرفتن اولین آدرس کاربر برای صفحه
    /// Checkout
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AddressInCheckoutPageViewModel> GetAddressForCheckoutPage(long userId);
}