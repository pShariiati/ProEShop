using ProEShop.Entities;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Services.Contracts;

public interface IUserListProductService : ICustomGenericService<UserListProduct>
{
    /// <summary>
    /// تمامی لیست هایی که این محصول داخلشون وجود داره
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="userListIds"></param>
    /// <returns></returns>
    Task<List<Entities.UserListProduct>> GetUserListProducts(long productId, List<long> userListIds);
}