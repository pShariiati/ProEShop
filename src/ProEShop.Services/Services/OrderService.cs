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

    public Task<Order> FindByOrderNumberAndIncludeParcelPosts(long orderNumber, long userId)
    {
        return _orders.Include(x => x.ParcelPosts)
            .Where(x => x.OrderNumber == orderNumber)
            .SingleOrDefaultAsync(x => x.UserId == userId);
    }
}
