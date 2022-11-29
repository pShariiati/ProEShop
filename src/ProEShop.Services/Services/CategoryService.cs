﻿using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;

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

    public async Task<bool> CheckProductCategoryIdsInComparePage(params int[] input)
    {
        var productCodes = await _products.Where(x => input.Contains(x.ProductCode))
            .Select(x => x.ProductCode).ToListAsync();

        // آیا رکوردی وجود دارد یا نه ؟

        if (productCodes.Count < 1)
        {
            return false;
        }

        // آیا تمامی آیتم های داخل لیست مشابه هم هستند یا نه ؟
        return productCodes.Distinct().Count() == 1;
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
