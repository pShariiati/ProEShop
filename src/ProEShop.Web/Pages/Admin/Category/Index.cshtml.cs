using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Web.Pages.Admin.Category;

public class IndexModel : PageModel
{
    #region Constructor

    private readonly ICategoryService _categoryService;

    public IndexModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    #endregion

    public SearchCategoriesViewModel SearchCategories { get; set; }
    = new();

    public ShowCategoriesViewModel Categories { get; set; }
    = new();

    public void OnGet()
    {

    }

    public async Task<PartialViewResult> OnGetGetDataTableAsync(SearchCategoriesViewModel searchCategories)
    {
        return Partial("List", await _categoryService.GetCategories(searchCategories));
    }
}