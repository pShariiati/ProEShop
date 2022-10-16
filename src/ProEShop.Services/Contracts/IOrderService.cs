using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;

namespace ProEShop.Services.Contracts;

public interface IOrderService : IGenericService<Order>
{
    /// <summary>
    /// استفاده شده در بخش ایجاد سفارش
    /// گرفتن آخرین شماره سفارش
    /// </summary>
    /// <returns></returns>
    Task<int> GetOrderNumberForCreateOrderAndPay();
}