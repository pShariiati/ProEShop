using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
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

        #region Search
        var searchedFullName = model.SearchSellers.FullName;
        if (!string.IsNullOrWhiteSpace(searchedFullName))
        {
            sellers = sellers.Where(x => (x.User.FirstName + " " + x.User.LastName).Contains(searchedFullName));
        }

        var searchedSellerCode = model.SearchSellers.SellerCode.ToString();
        if (!string.IsNullOrWhiteSpace(searchedSellerCode))
        {
            sellers = sellers.Where(x => x.SellerCode.ToString().Contains(searchedSellerCode));
        }

        var searchedShopName = model.SearchSellers.ShopName;
        if (!string.IsNullOrWhiteSpace(searchedShopName))
        {
            sellers = sellers.Where(x => x.ShopName.Contains(searchedShopName));
        }

        switch (model.SearchSellers.IsRealPersonStatus)
        {
            case IsRealPersonStatus.IsRealPerson:
                sellers = sellers.Where(x => x.IsRealPerson);
                break;
            case IsRealPersonStatus.IsLegalPerson:
                sellers = sellers.Where(x => !x.IsRealPerson);
                break;
        }

        switch (model.SearchSellers.IsActiveStatus)
        {
            case IsActiveStatus.Active:
                sellers = sellers.Where(x => x.IsActive);
                break;
            case IsActiveStatus.Disabled:
                sellers = sellers.Where(x => !x.IsActive);
                break;
        }

        var searchedDocumentStatus = model.SearchSellers.DocumentStatus;
        if (searchedDocumentStatus != null)
        {
            sellers = sellers.Where(x => x.DocumentStatus == searchedDocumentStatus);
        }
        #endregion

        #region OrderBy

        if (model.SearchSellers.Sorting == SortingSellers.Province)
        {
            if (model.SearchSellers.SortingOrder == SortingOrder.Asc)
            {
                sellers = sellers.OrderBy(x => x.Province.Title);
            }
            else
            {
                sellers = sellers.OrderByDescending(x => x.Province.Title);
            }
        }
        else if (model.SearchSellers.Sorting == SortingSellers.City)
        {
            if (model.SearchSellers.SortingOrder == SortingOrder.Asc)
            {
                sellers = sellers.OrderBy(x => x.City.Title);
            }
            else
            {
                sellers = sellers.OrderByDescending(x => x.City.Title);
            }
        }
        else if (model.SearchSellers.Sorting == SortingSellers.FullName)
        {
            if (model.SearchSellers.SortingOrder == SortingOrder.Asc)
            {
                sellers = sellers.OrderBy(x => x.User.FirstName + " " + x.User.LastName);
            }
            else
            {
                sellers = sellers.OrderByDescending(x => x.User.FirstName + " " + x.User.LastName);
            }
        }
        else
        {
            sellers = sellers.CreateOrderByExpression(model.SearchSellers.Sorting.ToString(),
                model.SearchSellers.SortingOrder.ToString());
        }

        #endregion

        var paginationResult = await GenericPaginationAsync(sellers, model.Pagination);

        return new()
        {
            Sellers = await _mapper.ProjectTo<ShowSellerViewModel>(
                    paginationResult.Query)
                .ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public async Task<SellerDetailsViewModel> GetSellerDetails(long id)
    {
        return await _mapper.ProjectTo<SellerDetailsViewModel>(
            _sellers
        ).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Seller> GetSellerToRemoveInManagingSellers(long id)
    {
        return await _sellers.Where(x => x.DocumentStatus == DocumentStatus.AwaitingInitialApproval)
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<long> GetSellerId(long userId)
    {
        var seller = await _sellers
            .Select(x => new
            {
                x.Id,
                x.UserId
            })
            .SingleAsync(x => x.UserId == userId);
        return seller.Id;
    }

    public Task<List<string>> GetShopNamesForAutocomplete(string input)
    {
        return _sellers.Where(x => x.ShopName.Contains(input))
            .Take(20)
            .Select(x => x.ShopName)
            .ToListAsync();
    }
}
