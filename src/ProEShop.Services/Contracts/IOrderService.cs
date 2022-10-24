using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;

namespace ProEShop.Services.Contracts;

public interface IOrderService : IGenericService<Order>
{
    /// <summary>
    /// گرفتن سفارش به همراه مرسوله های آن
    /// </summary>
    /// <param name="orderNumber"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<Order> FindByOrderNumberAndIncludeParcelPosts(long orderNumber, long userId);
}