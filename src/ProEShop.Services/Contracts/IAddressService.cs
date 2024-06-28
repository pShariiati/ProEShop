using ProEShop.Entities;
using ProEShop.ViewModels.Addresses;
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

    /// <summary>
    /// گرفتن آدرس کاربر برای بخش ایجاد سفارش
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<(bool HasUserAddress, long AddressId)> GetAddressForCreateOrderAndPay(long userId);

    /// <summary>
    /// گرفتن تمامی آدرس های کاربر
    /// </summary>
    /// <returns></returns>
    Task<List<ShowAddressInProfileViewModel>> GetAllUserAddresses();

    /// <summary>
    /// حذف آدرس کاربر
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> RemoveUserAddress(long id);
}