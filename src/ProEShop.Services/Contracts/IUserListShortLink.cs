using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;

namespace ProEShop.Services.Contracts;

public interface IUserListShortLinkService : IGenericService<UserListShortLink>
{
    /// <summary>
    /// گرفتن لینک برای بخش افزودن لیست
    /// </summary>
    /// <returns></returns>
    Task<UserListShortLink> GetUserListShortLinkForCreateUserList();
}