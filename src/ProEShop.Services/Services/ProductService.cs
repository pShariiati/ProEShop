using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.Search;
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

    public async Task<AddVariantViewModel> GetProductInfoForAddVariant(long productId)
    {
        var sellerId = await _sellerService.GetSellerId();

        return await _products
            .AsNoTracking()
            .AsSplitQuery()
            .ProjectTo<AddVariantViewModel>(
                configuration: _mapper.ConfigurationProvider,
                parameters: new { sellerId = sellerId }
            ).SingleOrDefaultAsync(x => x.ProductId == productId);
    }

    public Task<ShowProductInfoViewModel> GetProductInfo(long productCode)
    {
        long userId = 0;
        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            userId = _httpContextAccessor.HttpContext.User.Identity.GetLoggedInUserId();
        }

        return _products
            .AsNoTracking()
            .AsSplitQuery()
            .ProjectTo<ShowProductInfoViewModel>(
                configuration: _mapper.ConfigurationProvider,
                parameters: new { userId = userId, now = DateTime.Now }
            ).SingleOrDefaultAsync(x => x.ProductCode == productCode);
    }

    public async Task<(int productCode, string slug)> FindByShortLink(string productShortLint)
    {
        var productShortLink = await _products
            .Select(x => new
            {
                x.Slug,
                x.ProductCode,
                x.ProductShortLink
            }).SingleOrDefaultAsync(x =>
                    x.ProductShortLink.Link == productShortLint
                );
        return (
            productShortLink?.ProductCode ?? 0,
            productShortLink?.Slug
        );
    }

    public Task<List<Product>> GetProductsForChangeStatus(List<long> ids)
    {
        return _products.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public Task<List<ShowProductInCompareViewModel>> GetProductsForCompare(params int[] productCodes)
    {
        productCodes = productCodes.Where(x => x > 0).ToArray();

        return _mapper.ProjectTo<ShowProductInCompareViewModel>(
            _products.Where(x => productCodes.Contains(x.ProductCode))
        ).ToListAsync();
    }

    public async Task<ShowProductInComparePartialViewModel> GetProductsForAddProductInCompare(int pageNumber, string searchValue, int[] productCodesToHide)
    {
        var result = new ShowProductInComparePartialViewModel();

        searchValue = searchValue?.Trim() ?? string.Empty;

        if (pageNumber < 1)
            pageNumber = 1;

        var firstProductCategoryId = await GetProductCategoryId(productCodesToHide.First());

        var query = _products
            .Where(x =>
                searchValue == ""
                ||
                    (
                        x.PersianTitle.Contains(searchValue)
                        ||
                        x.EnglishTitle.Contains(searchValue)
                    )
                )
            .AsNoTracking()
            .Where(x => productCodesToHide.Contains(x.ProductCode) == false)
            .Where(x => x.MainCategoryId == firstProductCategoryId)
            .OrderBy(x => x.Id);

        var itemsCount = await query.LongCountAsync();

        var take = 3;

        var pagesCount = (int)Math.Ceiling(
            (decimal)itemsCount / take
        );

        if (pagesCount <= 0)
            pagesCount = 1;

        if (pageNumber >= pagesCount)
        {
            result.IsLastPage = true;
            pageNumber = pagesCount;
        }

        var skip = (pageNumber - 1) * take;

        result.Products = await _mapper.ProjectTo<ProductItemForShowProductInComparePartialViewModel>(
                query.Skip(skip).Take(take)
            ).ToListAsync();

        result.PageNumber = pageNumber;
        result.Count = itemsCount;

        return result;
    }

    public Task<long> GetProductCategoryId(long productCode)
    {
        return _products.Where(x => x.ProductCode == productCode)
            .Select(x => x.MainCategoryId)
            .SingleOrDefaultAsync();
    }

    public async Task<ShowProductsInSearchOnCategoryViewModel> GetProductsByPaginationForSearch(SearchOnCategoryInputsViewModel inputs)
    {
        var result = new ShowProductsInSearchOnCategoryViewModel();

        if (inputs.PageNumber < 1)
            inputs.PageNumber = 1;

        var productQuery = _products
            .AsNoTracking()
            .Where(x => x.Category.Slug == inputs.CategorySlug);

        #region Search

        if (inputs.Brands is { Count: > 0 })
        {
            productQuery = productQuery.Where(x => inputs.Brands.Contains(x.BrandId));
        }

        if (inputs.Variants is { Count: > 0 })
        {
            productQuery = productQuery.Where(x => x.ProductVariants
                .Where(pv => pv.Count > 0)
                .Any(pv => inputs.Variants.Contains(pv.VariantId.Value))
            );
        }

        // min: 1000
        // max: 900
        // مین نباید بیشتر از مکس باشد
        // مین باید حداقل مساوی یا کمتر باشد
        // یا
        // اگر مقدار مکسیموم تغییر نکند، کلاینت، مقدار صفر را به سمت سرور ارسال میکند
        // پس اگر فقط مقدار مینیموم را تغییر دهیم و به مکسیموم کاری نداشته باشیم
        // چون مکسیموم به صورت صفر به سمت سرور میاید در نتیجه مکسیموم از مینیموم بزرگتر میشود
        // پس حالت "یا" را به این خاطر مینویسیم
        if (inputs.MaximumPrice >= inputs.MinimumPrice || inputs.MaximumPrice == 0 && inputs.MinimumPrice > 0)
        {
            if (inputs.MinimumPrice > 0)
            {
                productQuery = productQuery.Where(x => x.Price >= inputs.MinimumPrice);
            }

            if (inputs.MaximumPrice > 0)
            {
                productQuery = productQuery.Where(x => x.Price <= inputs.MaximumPrice);
            }
        }

        if (inputs.OnlyExistsProducts)
        {
            productQuery = productQuery.Where(x => x.ProductStockStatus == ProductStockStatus.Available);
        }

        if (inputs.Features is { Count: > 0 })
        {
            foreach (var feature in inputs.Features)
            {
                if (!string.IsNullOrWhiteSpace(feature))
                {
                    // 3___6گیگ|||
                    var splitFeature = feature.Split("___");
                    if (long.TryParse(splitFeature.First(), out long featureId))
                    {
                        var featureValues = splitFeature.Last().Split("|||",
                            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                        if (featureValues.Length > 0)
                        {
                            productQuery = productQuery.Where(x => x.ProductFeatures
                                .Where(pf => pf.FeatureId == featureId)
                                .Any(pf => featureValues.Contains(pf.Value)));
                        }
                    }
                }
            }
        }

        #endregion

        #region OrderBy

        if (inputs.Sorting == SortingSearchOnCategory.BuyersSuggest)
        {
            productQuery = productQuery.OrderByDescending(x => x.ProductComments.LongCount(pc => pc.Suggest.Value));
        }
        else
        {
            productQuery = productQuery.CreateOrderByExpression(inputs.Sorting.ToString(),
                inputs.SortingOrder.ToString());
        }

        #endregion

        var itemsCount = await productQuery.LongCountAsync();

        const byte take = 2;

        var pagesCount = (int)Math.Ceiling(
            (decimal)itemsCount / take
        );

        if (pagesCount <= 0)
            pagesCount = 1;

        if (inputs.PageNumber >= pagesCount)
        {
            inputs.PageNumber = pagesCount;
        }

        var skip = (inputs.PageNumber - 1) * take;

        result.AllProductsCount = await productQuery.LongCountAsync();

        result.Products = await _mapper.ProjectTo<ShowProductInSearchOnCategoryViewModel>(
            productQuery.Skip(skip).Take(take)
        ).ToListAsync();

        result.CurrentPage = inputs.PageNumber;
        result.PagesCount = pagesCount;

        return result;
    }
}
