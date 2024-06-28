using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Search;

namespace ProEShop.Services.Services;

public class CategoryService : GenericService<Category>, ICategoryService
{
    private readonly DbSet<Category> _categories;
    private readonly DbSet<Product> _products;
    private readonly IMapper _mapper;
    private readonly ISellerService _sellerService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CategoryService(
        IUnitOfWork uow,
        IMapper mapper,
        ISellerService sellerService,
        IHttpContextAccessor httpContextAccessor)
        : base(uow)
    {
        _mapper = mapper;
        _sellerService = sellerService;
        _httpContextAccessor = httpContextAccessor;
        _categories = uow.Set<Category>();
        _products = uow.Set<Product>();
    }

    public async Task<ShowCategoriesViewModel> GetCategories(ShowCategoriesViewModel model)
    {
        var categories = _categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(model.SearchCategories.Title))
            categories = categories.Where(x => x.Title.Contains(model.SearchCategories.Title.Trim()));

        if (!string.IsNullOrWhiteSpace(model.SearchCategories.Slug))
            categories = categories.Where(x => x.Slug.Contains(model.SearchCategories.Slug.Trim()));

        switch (model.SearchCategories.DeletedStatus)
        {
            case ViewModels.DeletedStatus.True:
                break;
            case ViewModels.DeletedStatus.OnlyDeleted:
                categories = categories.Where(x => x.IsDeleted);
                break;
            default:
                categories = categories.Where(x => !x.IsDeleted);
                break;
        }

        switch (model.SearchCategories.ShowInMenusStatus)
        {
            case ShowInMenusStatus.True:
                categories = categories.Where(x => x.ShowInMenus);
                break;
            case ShowInMenusStatus.False:
                categories = categories.Where(x => !x.ShowInMenus);
                break;
            default:
                break;
        }

        categories = categories.CreateOrderByExpression(model.SearchCategories.Sorting.ToString(),
            model.SearchCategories.SortingOrder.ToString());

        var paginationResult = await GenericPaginationAsync(categories, model.Pagination);

        return new()
        {
            Categories = await paginationResult.Query
            .Select(x => new ShowCategoryViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ShowInMenus = x.ShowInMenus,
                Parent = x.ParentId != null ? x.Parent.Title : "دسته اصلی",
                Slug = x.Slug,
                Picture = x.Picture ?? "بدون عکس",
                IsDeleted = x.IsDeleted,
                ShowEditVariantButton = x.IsVariantColor != null
            })
            .ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public async Task<Dictionary<long, string>> GetCategoriesToShowInSelectBoxAsync(long? id = null)
    {
        return await _categories
            .Where(x => id == null || x.Id != id)
            .ToDictionaryAsync(x => x.Id, x => x.Title);
    }

    public Task<Dictionary<long, string>> GetCategoriesWithNoChild()
    {
        return _categories.Where(x => !x.Categories.Any())
            .ToDictionaryAsync(x => x.Id, x => x.Title);
    }

    public async Task<EditCategoryViewModel> GetForEdit(long id)
    {
        return await _mapper.ProjectTo<EditCategoryViewModel>(
            _categories
        ).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<List<ShowCategoryForCreateProductViewModel>>> GetCategoriesForCreateProduct(long[] selectedCategoriesIds)
    {
        var result = new List<List<ShowCategoryForCreateProductViewModel>>
        {
            await _categories.Where(x => x.ParentId == null)
                .Select(x => new ShowCategoryForCreateProductViewModel()
                {
                    Title = x.Title,
                    HasChild = x.Categories.Any(),
                    Id = x.Id
                }).ToListAsync()
        };
        for (var counter = 0; counter < selectedCategoriesIds.Length; counter++)
        {
            var selectedCategoryId = selectedCategoriesIds[counter];
            result.Add(
                await _categories.Where(x => x.ParentId == selectedCategoryId)
                    .Select(x => new ShowCategoryForCreateProductViewModel()
                    {
                        Title = x.Title,
                        HasChild = x.Categories.Any(),
                        Id = x.Id
                    }).ToListAsync()
            );
        }

        return result;
    }

    public async Task<List<string>> GetCategoryBrands(long categoryId)
    {
        return await _categories
            .Where(x => x.Id == categoryId)
            .SelectMany(x => x.CategoryBrands)
            .Select(x => x.Brand.TitleFa + " " + x.Brand.TitleEn + "|||" + x.CommissionPercentage)
            .ToListAsync();
    }

    public Task<Category> GetCategoryWithItsBrands(long categoryId)
    {
        return _categories
            .Include(x => x.CategoryBrands)
            .SingleOrDefaultAsync(x => x.Id == categoryId);
    }

    public async Task<bool> CanAddFakeProduct(long categoryId)
    {
        var category = await _categories.Select(x => new
        {
            x.Id,
            x.CanAddFakeProduct
        }).SingleOrDefaultAsync(x => x.Id == categoryId);
        return category?.CanAddFakeProduct ?? false;
    }

    public async Task<(bool isSuccessful, List<long> categoryIds)> GetCategoryParentIds(long categoryId)
    {
        if (!await IsExistsBy(nameof(Entities.Category.Id), categoryId))
        {
            return (false, new List<long>());
        }

        if (await _categories.AnyAsync(x => x.ParentId == categoryId))
        {
            return (false, new List<long>());
        }

        var result = new List<long>() { categoryId };
        var currentCategoryId = categoryId;
        while (true)
        {
            var categoryToAdd = await _categories
                .Select(x => new
                {
                    x.Id,
                    x.ParentId
                })
                .SingleOrDefaultAsync(x => x.Id == currentCategoryId);
            if (categoryToAdd.ParentId is null)
            {
                break;
            }

            currentCategoryId = categoryToAdd.ParentId.Value;
            result.Add(categoryToAdd.ParentId.Value);
        }

        return (true, result);
    }

    public async Task<Dictionary<long, string>> GetSellerCategories()
    {
        var userId = _httpContextAccessor.HttpContext.User.Identity.GetLoggedInUserId();
        var sellerId = await _sellerService.GetSellerId(userId);
        return await _products.Where(x => x.SellerId == sellerId)
            .GroupBy(x => x.MainCategoryId)
            .Select(x => new
            {
                x.Key,
                x.First().Category.Title
            }).ToDictionaryAsync(x => x.Key, x => x.Title);
    }

    public Task<bool?> IsVariantTypeColor(long categoryId)
    {
        return _categories.Where(x => x.Id == categoryId)
            .Select(x => x.IsVariantColor)
            .SingleOrDefaultAsync();
    }

    public Task<Category> GetCategoryForEditVariant(long categoryId)
    {
        return _categories.Include(x => x.CategoryVariants)
            .SingleOrDefaultAsync(x => x.Id == categoryId);
    }

    public async Task<bool> CheckProductCategoryIdsInComparePage(params int[] productCodes)
    {
        var mainCategoryIds = await _products.Where(x => productCodes.Contains(x.ProductCode))
            .Select(x => x.MainCategoryId).ToListAsync();

        // آیا رکوردی وجود دارد یا نه ؟

        if (mainCategoryIds.Count < 1)
        {
            return false;
        }

        // آیا تمامی دسته بندی های محصولات داخل لیست مشابه هم هستند یا نه ؟
        return mainCategoryIds.Distinct().Count() == 1;
    }

    public Task<string> GetCategoryTitle(long categoryId)
    {
        return _categories.Where(x => x.Id == categoryId)
            .Select(x => x.Title)
            .SingleAsync();
    }

    public async Task<SearchOnCategoryViewModel> GetSearchOnCategoryData(string categorySlug, string brandSlug)
    {
        var result = await _categories
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.Slug == categorySlug)
            .ProjectTo<SearchOnCategoryViewModel>(
                configuration: _mapper.ConfigurationProvider,
                parameters: new { brandSlug }
            ).SingleOrDefaultAsync();
        if (result is null)
        {
            return null;
        }

        // https://www.dntips.ir/post/3234
        var parentsResult = await _categories
            .Where(x => x.Slug == categorySlug
                             || x.Categories.Any(m => x.Id == m.ParentId))
            .ToListAsync(); //It's a MUST - get all children from the database

        var mainCategory = parentsResult.First(x => x.Slug == categorySlug);

        var actualResult = new List<BreadcrumbItemInSearchOnCategoryViewModel>();
        FindParents(mainCategory, actualResult);
        actualResult.Reverse();

        // چون دسته اصلی اضافه نمیشود پس دسته اصلی را هم در آخر سر اضافه میکنیم
        actualResult.Add(new()
        {
            Slug = mainCategory.Slug,
            Title = mainCategory.Title
        });
        result.BreadcrumbItems = actualResult;

        return result;
    }

    private static void FindParents(Category category, List<BreadcrumbItemInSearchOnCategoryViewModel> actualResult)
    {
        if (category == null || category.Parent == null)
        {
            return;
        }

        var item = category.Parent;
        actualResult.Add(new ()
        {
            Slug = item.Slug,
            Title = item.Title
        });

        if (item.Parent != null)
        {
            FindParents(item, actualResult);
        }
    }

    public override async Task<DuplicateColumns> AddAsync(Category entity)
    {
        var result = new List<string>();
        if (await _categories.AnyAsync(x => x.Title == entity.Title))
            result.Add(nameof(Category.Title));
        if (await _categories.AnyAsync(x => x.Slug == entity.Slug))
            result.Add(nameof(Category.Slug));
        if (!result.Any())
            await base.AddAsync(entity);
        return new(!result.Any())
        {
            Columns = result
        };
    }

    public override async Task<DuplicateColumns> Update(Category entity)
    {
        var query = _categories.Where(x => x.Id != entity.Id);
        var result = new List<string>();
        if (await query.AnyAsync(x => x.Title == entity.Title))
            result.Add(nameof(Category.Title));
        if (await query.AnyAsync(x => x.Slug == entity.Slug))
            result.Add(nameof(Category.Slug));
        if (!result.Any())
            await base.Update(entity);
        return new(!result.Any())
        {
            Columns = result
        };
    }
}
