using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Services.Services;

public class SellerService : GenericService<Seller>, ISellerService
{
    private readonly DbSet<Seller> _sellers;
    private readonly IMapper _mapper;

    public SellerService(IUnitOfWork uow, IMapper mapper) : base(uow)
    {
        _mapper = mapper;
        _sellers = uow.Set<Seller>();
    }

    public override async Task<DuplicateColumns> AddAsync(Seller entity)
    {
        var result = new List<string>();
        if (await _sellers.AnyAsync(x => x.ShabaNumber == entity.ShabaNumber))
            result.Add(nameof(Seller.ShabaNumber));
        if (await _sellers.AnyAsync(x => x.ShopName == entity.ShopName))
            result.Add(nameof(Seller.ShopName));
        if (!result.Any())
            await base.AddAsync(entity);
        return new(!result.Any())
        {
            Columns = result
        };
    }

    public async Task<int> GetSellerCodeForCreateSeller()
    {
        var latestSellerCode = await _sellers.OrderByDescending(x => x.Id)
            .Select(x => x.SellerCode)
            .FirstOrDefaultAsync();
        return latestSellerCode + 1;
    }

    public async Task<ShowSellersViewModel> GetSellers(ShowSellersViewModel model)
    {
        var sellers = _sellers.AsQueryable();

        sellers = sellers.CreateOrderByExpression(model.SearchSellers.Sorting.ToString(),
            model.SearchSellers.SortingOrder.ToString());

        var paginationResult = await GenericPaginationAsync(sellers, model.Pagination);

        return new()
        {
            Sellers = await _mapper.ProjectTo<ShowSellerViewModel>(
                    paginationResult.Query)
                .ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }
}
