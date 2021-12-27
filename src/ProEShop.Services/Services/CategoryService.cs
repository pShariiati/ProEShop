using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Services.Services;

public class CategoryService : GenericService<Category>, ICategoryService
{
    private readonly DbSet<Category> _categories;
    public CategoryService(IUnitOfWork uow)
        : base(uow)
    {
        _categories = uow.Set<Category>();
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
                IsDeleted = x.IsDeleted
            })
            .ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public Dictionary<long, string> GetCategoriesToShowInSelectBox(long? id = null)
    {
        return _categories
            .Where(x => id == null || x.Id != id)
            .ToDictionary(x => x.Id, x => x.Title);
    }

    public async Task<EditCategoryViewModel> GetForEdit(long id)
    {
        return await _categories.Select(x => new EditCategoryViewModel()
        {
            SelectedPicture = x.Picture,
            ParentId = x.ParentId,
            Id = x.Id,
            Description = x.Description,
            Title = x.Title,
            Slug = x.Slug,
            ShowInMenus = x.ShowInMenus
        }).SingleOrDefaultAsync(x => x.Id == id);
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
