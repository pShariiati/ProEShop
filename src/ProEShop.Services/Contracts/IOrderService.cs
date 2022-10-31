using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Orders;

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

    /// <summary>
    /// نمایش تمامی سفارشات در داخل گرید
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ShowOrdersViewModel> GetOrders(ShowOrdersViewModel model);
}