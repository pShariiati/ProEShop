using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Services.Services;

public class ConsignmentService : GenericService<Consignment>, IConsignmentService
{
    private readonly DbSet<Consignment> _consignments;
    private readonly IMapper _mapper;

    public ConsignmentService(
        IUnitOfWork uow,
        IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _consignments = uow.Set<Consignment>();
    }


    public async Task<ShowConsignmentsViewModel> GetConsignments(ShowConsignmentsViewModel model)
    {
        var consignments = _consignments.AsNoTracking().AsQueryable();

        #region Search

        var searchedShopName = model.SearchConsignments.ShopName;
        if (!string.IsNullOrWhiteSpace(searchedShopName))
        {
            consignments = consignments.Where(x => x.Seller.ShopName.Contains(searchedShopName));
        }

        consignments = ExpressionHelpers.CreateSearchExpressions(consignments, model.SearchConsignments, false);

        #endregion

        #region OrderBy

        var sorting = model.SearchConsignments.Sorting;
        var isSortingAsc = model.SearchConsignments.SortingOrder == SortingOrder.Asc;
        if (sorting == SortingConsignments.ShopName)
        {
            if (isSortingAsc)
                consignments = consignments.OrderBy(x => x.Seller.ShopName);
            else
                consignments = consignments.OrderByDescending(x => x.Seller.ShopName);
        }
        else
        {
            consignments = consignments.CreateOrderByExpression(model.SearchConsignments.Sorting.ToString(),
            model.SearchConsignments.SortingOrder.ToString());
        }

        #endregion

        var paginationResult = await GenericPaginationAsync(consignments, model.Pagination);

        return new()
        {
            Consignments = await _mapper.ProjectTo<ShowConsignmentViewModel>(
                    paginationResult.Query)
                .ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public Task<Consignment> GetConsignmentForConfirmation(long consignmentId)
    {
        return _consignments.Where(x => x.ConsignmentStatus == ConsignmentStatus.AwaitingApproval)
            .SingleOrDefaultAsync(x => x.Id == consignmentId);
    }

    public Task<ShowConsignmentDetailsViewModel> GetConsignmentDetails(long consignmentId)
    {
        return _consignments.ProjectTo<ShowConsignmentDetailsViewModel>(
                configuration: _mapper.ConfigurationProvider, parameters: new { consignmentId = consignmentId })
            .SingleOrDefaultAsync(x => x.Id == consignmentId);
    }
}
