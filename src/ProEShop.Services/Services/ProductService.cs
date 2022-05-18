using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Services;

public class ProductService : GenericService<Product>, IProductService
{
    private readonly DbSet<Product> _products;
    private readonly IMapper _mapper;

    public ProductService(
        IUnitOfWork uow,
        IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _products = uow.Set<Product>();
    }

    public async Task<ShowProductsViewModel> GetProducts(ShowProductsViewModel model)
    {
        var products = _products.AsNoTracking().AsQueryable();

        #region Search

        var searchedShopName = model.SearchProducts.ShopName;
        if (!string.IsNullOrWhiteSpace(searchedShopName))
        {
            products = products.Where(x => x.Seller.ShopName.Contains(searchedShopName));
        }

        var searchedStatus = model.SearchProducts.Status;
        if (searchedStatus is not null)
        {
            products = products.Where(x => x.Status == searchedStatus);
        }

        products = ExpressionHelpers.CreateSearchExpressions(products, model.SearchProducts);

        #endregion

        #region OrderBy

        var sorting = model.SearchProducts.Sorting;
        var isSortingAsc = model.SearchProducts.SortingOrder == SortingOrder.Asc;
        if (sorting == SortingProducts.ShopName)
        {
            if (isSortingAsc)
                products = products.OrderBy(x => x.Seller.ShopName);
            else
                products = products.OrderByDescending(x => x.Seller.ShopName);
        }
        else if (sorting == SortingProducts.BrandFa)
        {
            if (isSortingAsc)
                products = products.OrderBy(x => x.Brand.TitleFa);
            else
                products = products.OrderByDescending(x => x.Brand.TitleFa);
        }
        else if (sorting == SortingProducts.BrandEn)
        {
            if (isSortingAsc)
                products = products.OrderBy(x => x.Brand.TitleEn);
            else
                products = products.OrderByDescending(x => x.Brand.TitleEn);
        }
        else
        {
            products = products.CreateOrderByExpression(model.SearchProducts.Sorting.ToString(),
            model.SearchProducts.SortingOrder.ToString());
        }

        #endregion

        var paginationResult = await GenericPaginationAsync(products, model.Pagination);

        return new()
        {
            Products = await _mapper.ProjectTo<ShowProductViewModel>(
                    paginationResult.Query)
                .ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public Task<List<string>> GetPersianTitlesForAutocomplete(string input)
    {
        return _products.Where(x => x.PersianTitle.Contains(input))
            .Take(20)
            .Select(x => x.PersianTitle)
            .ToListAsync();
    }
}
