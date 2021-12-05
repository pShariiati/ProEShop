using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Web.Pages.Admin.Category;

public class IndexModel : PageBase
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

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnGetGetDataTableAsync(SearchCategoriesViewModel searchCategories)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _categoryService.GetCategories(searchCategories));
    }

    public IActionResult OnGetAdd()
    {
        return Partial("Add");
    }

    public IActionResult OnPostAdd(AddCategoryViewModel model)
    {
        return Partial("Add");
    }
}