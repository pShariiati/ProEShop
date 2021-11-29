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

    public async Task<ShowCategoriesViewModel> GetCategories()
    {
        var categories = await _categories
			.IgnoreQueryFilters()
            .Select(x => new ShowCategoryViewModel
            {
                Title = x.Title,
                ShowInMenus = x.ShowInMenus,
                Parent = x.ParentId != null ? x.Parent.Title : "دسته اصلی"
            })
            .ToListAsync();

        return new()
        {
            Categories = categories
        };
    }
}
