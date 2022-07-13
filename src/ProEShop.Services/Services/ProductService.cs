using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.Variants;

namespace ProEShop.Services.Services;

public class ProductService : GenericService<Product>, IProductService
{
    private readonly DbSet<Product> _products;
    private readonly IMapper _mapper;
    private readonly ISellerService _sellerService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService(
        IUnitOfWork uow,
        IMapper mapper,
        ISellerService sellerService,
        IHttpContextAccessor httpContextAccessor)
        : base(uow)
    {
        _mapper = mapper;
        _sellerService = sellerService;
        _httpContextAccessor = httpContextAccessor;
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

    public async Task<ShowProductsInSellerPanelViewModel> GetProductsInSellerPanel(ShowProductsInSellerPanelViewModel model)
    {
        var sellerId = await _sellerService.GetSellerId();
        var products = _products.AsNoTracking()
            .Where(x => x.SellerId == sellerId || x.ProductVariants
                .Any(pv => pv.SellerId == sellerId))
            .AsQueryable();

        #region Search

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
        if (sorting == SortingProductsInSellerPanel.BrandFa)
        {
            if (isSortingAsc)
                products = products.OrderBy(x => x.Brand.TitleFa);
            else
                products = products.OrderByDescending(x => x.Brand.TitleFa);
        }
        else if (sorting == SortingProductsInSellerPanel.BrandEn)
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
            Products = await _mapper.ProjectTo<ShowProductInSellerPanelViewModel>(
                    paginationResult.Query)
                .ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public async Task<ShowAllProductsInSellerPanelViewModel> GetAllProductsInSellerPanel(ShowAllProductsInSellerPanelViewModel model)
    {
        var products = _products
            .Where(x => x.Status == ProductStatus.Confirmed)
            .AsNoTracking().AsQueryable();

        #region Search

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
        if (sorting == SortingAllProductsInSellerPanel.BrandFa)
        {
            if (isSortingAsc)
                products = products.OrderBy(x => x.Brand.TitleFa);
            else
                products = products.OrderByDescending(x => x.Brand.TitleFa);
        }
        else if (sorting == SortingAllProductsInSellerPanel.BrandEn)
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
            Products = await _mapper.ProjectTo<ShowAllProductInSellerPanelViewModel>(
                    paginationResult.Query)
                .ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public Task<List<string>> GetPersianTitlesForAutocomplete(string input)
    {
        return _products.Where(x => x.PersianTitle.Contains(input))
            .AsNoTracking()
            .Take(20)
            .Select(x => x.PersianTitle)
            .ToListAsync();
    }

    public Task<ProductDetailsViewModel> GetProductDetails(long productId)
    {
        return _mapper.ProjectTo<ProductDetailsViewModel>(
            _products
                .AsNoTracking()
                .AsSplitQuery()
            ).SingleOrDefaultAsync(x => x.Id == productId);
    }

    public Task<Product> GetProductToRemoveInManagingProducts(long id)
    {
        return _products.Where(x => x.Status == ProductStatus.AwaitingInitialApproval)
            .Include(x => x.ProductMedia)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> GetProductCodeForCreateProduct()
    {
        var lastProductNumber = await _products.OrderByDescending(x => x.Id)
            .Select(x => x.ProductCode)
            .FirstOrDefaultAsync();
        return lastProductNumber + 1;
    }

    public async Task<List<string>> GetPersianTitlesForAutocompleteInSellerPanel(string input)
    {
        var userId = _httpContextAccessor.HttpContext.User.Identity.GetLoggedInUserId();
        var sellerId = await _sellerService.GetSellerId(userId);
        return await _products.Where(x => x.PersianTitle.Contains(input))
            .Where(x => x.SellerId == sellerId)
            .AsNoTracking()
            .Take(20)
            .Select(x => x.PersianTitle)
            .ToListAsync();
    }

    public Task<AddVariantViewModel> GetProductInfoForAddVariant(long productId)
    {
        return _mapper.ProjectTo<AddVariantViewModel>(
            _products
        ).SingleOrDefaultAsync(x => x.ProductId == productId);
    }

    public Task<ShowProductInfoViewModel> GetProductInfo(long productCode)
    {
        return _mapper.ProjectTo<ShowProductInfoViewModel>(
                _products
            )
            .SingleOrDefaultAsync(x => x.ProductCode == productCode);
    }
}
