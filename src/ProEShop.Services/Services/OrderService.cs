using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Services.Services;

public class OrderService : GenericService<Order>, IOrderService
{
    private readonly DbSet<Order> _orders;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork uow)
        : base(uow)
    {
        _orders = uow.Set<Order>();
    }

    public async Task<int> GetOrderNumberForCreateOrderAndPay()
    {
        var lastOrderNumber = await _orders.OrderByDescending(x => x.Id)
            .Select(x => x.OrderNumber)
            .FirstOrDefaultAsync();

        return lastOrderNumber + 1;
    }
}
