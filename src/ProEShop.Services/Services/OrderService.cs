using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Services.Services;

public class OrderService : GenericService<Order>, IOrderService
{
    private readonly DbSet<Order> _orders;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _orders = uow.Set<Order>();
    }

    public Task<Order> FindByOrderNumberAndIncludeParcelPosts(long orderNumber, long userId)
    {
        return _orders.Include(x => x.ParcelPosts)
            .Where(x => x.OrderNumber == orderNumber)
            .SingleOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<ShowOrdersViewModel> GetOrders(ShowOrdersViewModel model)
    {
        var orders = _orders.AsNoTracking().AsQueryable();

        #region Search

        // We can't search (Contains) on [NotMapped] properties
        var searchedFullName = model.SearchOrders.FullName;
        if (!string.IsNullOrWhiteSpace(searchedFullName))
        {
            orders = orders.Where(x => (x.Address.FirstName + " " + x.Address.LastName).Contains(searchedFullName));
        }

        var searchedProvinceId = model.SearchOrders.ProvinceId;
        if (searchedProvinceId is > 0)
        {
            orders = orders.Where(x => x.Address.ProvinceId == searchedProvinceId);
        }

        var searchedCityId = model.SearchOrders.CityId;
        if (searchedCityId is > 0)
        {
            orders = orders.Where(x => x.Address.CityId == searchedCityId);
        }

        if (model.SearchOrders.IsPay)
        {
            orders = orders.Where(x => x.IsPay);
        }

        orders = ExpressionHelpers.CreateSearchExpressions(orders, model.SearchOrders, false);

        #endregion

        #region OrderBy

        orders = orders.CreateOrderByExpression(model.SearchOrders.Sorting.ToString(),
            model.SearchOrders.SortingOrder.ToString());

        #endregion

        var paginationResult = await GenericPaginationAsync(orders, model.Pagination);

        return new()
        {
            Orders = await _mapper.ProjectTo<ShowOrderViewModel>(
                paginationResult.Query
            ).ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public async Task<ShowOrdersInDeliveryOrdersViewModel> GetDeliveryOrders(ShowOrdersInDeliveryOrdersViewModel model)
    {
        var orders = _orders.Where(x => x.IsPay).AsNoTracking().AsQueryable();

        #region Search

        // We can't search (Contains) on [NotMapped] properties
        var searchedFullName = model.SearchOrders.FullName;
        if (!string.IsNullOrWhiteSpace(searchedFullName))
        {
            orders = orders.Where(x => (x.Address.FirstName + " " + x.Address.LastName).Contains(searchedFullName));
        }

        var searchedProvinceId = model.SearchOrders.ProvinceId;
        if (searchedProvinceId is > 0)
        {
            orders = orders.Where(x => x.Address.ProvinceId == searchedProvinceId);
        }

        var searchedCityId = model.SearchOrders.CityId;
        if (searchedCityId is > 0)
        {
            orders = orders.Where(x => x.Address.CityId == searchedCityId);
        }

        if (model.SearchOrders.Status is null)
        {
            orders = orders.Where(x => x.Status != OrderStatus.WaitingForPaying)
                .Where(x => x.Status != OrderStatus.Processing);
        }

        orders = ExpressionHelpers.CreateSearchExpressions(orders, model.SearchOrders, false);

        #endregion

        #region OrderBy

        orders = orders.CreateOrderByExpression(model.SearchOrders.Sorting.ToString(),
            model.SearchOrders.SortingOrder.ToString());

        #endregion

        var paginationResult = await GenericPaginationAsync(orders, model.Pagination);

        return new()
        {
            Orders = await _mapper.ProjectTo<ShowOrderInDeliveryOrdersViewModel>(
                paginationResult.Query
            ).ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public Task<OrderDetailsViewModel> GetOrderDetails(long orderId)
    {
        return _mapper.ProjectTo<OrderDetailsViewModel>(
            _orders
                .AsSplitQuery()
                .AsNoTracking())
            .SingleOrDefaultAsync(x => x.Id == orderId);
    }
}
