using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Orders;
using ProEShop.ViewModels.ProductComments;

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
    /// گرفتن سفارش بر اساس شماره سفارش
    /// </summary>
    /// <param name="orderNumber"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<VerifyPageDataViewModel> FindByOrderNumber(long orderNumber, long userId);

    /// <summary>
    /// نمایش تمامی سفارشات در داخل گرید
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ShowOrdersViewModel> GetOrders(ShowOrdersViewModel model);

    /// <summary>
    /// نمایش تمامی سفارشات در داخل گرید تحویل دادن مرسوله ها
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ShowOrdersInDeliveryOrdersViewModel> GetDeliveryOrders(ShowOrdersInDeliveryOrdersViewModel model);

    /// <summary>
    /// گرفتن جزییات سفارش
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<OrderDetailsViewModel> GetOrderDetails(long orderId);

    /// <summary>
    /// گرفتن جزییات سفارش در بخش پروفایل کاربری
    /// </summary>
    /// <param name="orderNumber"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<OrderDetailsViewModel> GetOrderDetailsInProfile(long orderNumber, long userId);

    /// <summary>
    /// نمایش تمامی سفارشات در بخش پروفایل
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ShowOrdersInProfileViewModel> GetOrdersInProfile(ShowOrdersInProfileViewModel model);

    /// <summary>
    /// این متد از متد بالایی استفاده میکند، اینجا فقط بهش شماره صحفه پاس میدیم
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    Task<ShowOrdersInProfileViewModel> GetOrdersInProfile(int pageNumber);
}