using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Search;

namespace ProEShop.Web.Pages.Search;

public class ShowCategoryModel : PageModel
{
    #region Constructor

    private readonly ICategoryService _categoryService;

    public ShowCategoryModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    #endregion

    public SearchOnCategoryViewModel SearchOnCategory { get; set; }

    public async Task OnGet(string categorySlug)
    {
        SearchOnCategory = await _categoryService.GetSearchOnCategoryData(categorySlug);
    }
}