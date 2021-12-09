using Microsoft.EntityFrameworkCore;
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

    public async Task<ShowCategoriesViewModel> GetCategories(SearchCategoriesViewModel model)
    {
        var categories = _categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(model.Title))
            categories = categories.Where(x => x.Title.Contains(model.Title.Trim()));

        if (!string.IsNullOrWhiteSpace(model.Slug))
            categories = categories.Where(x => x.Slug.Contains(model.Slug.Trim()));

        switch (model.DeletedStatus)
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

        switch (model.ShowInMenusStatus)
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

        return new()
        {
            Categories = await categories
            .Select(x => new ShowCategoryViewModel
            {
                Title = x.Title,
                ShowInMenus = x.ShowInMenus,
                Parent = x.ParentId != null ? x.Parent.Title : "دسته اصلی",
                Slug = x.Slug
            })
            .ToListAsync()
        };
    }

    public Dictionary<long, string> GetCategoriesToShowInSelectBox()
    {
        return _categories.ToDictionary(x => x.Id, x => x.Title);
    }
}
