using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Web.Pages.Admin.CategoryFeature;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly ICategoryFeatureService _categoryFeatureService;
    private readonly ICategoryService _categoryService;
    private readonly IUnitOfWork _uow;

    public IndexModel(
        ICategoryFeatureService categoryFeatureService,
        IUnitOfWork uow,
        ICategoryService categoryService)
    {
        _categoryFeatureService = categoryFeatureService;
        _uow = uow;
        _categoryService = categoryService;
    }

    #endregion

    public ShowCategoryFeaturesViewModel CategoryFeatures { get; set; }
        = new();

    public async Task OnGet()
    {
        var categories = await _categoryService.GetCategoriesToShowInSelectBoxAsync();
        CategoryFeatures.SearchCategoryFeatures.Categories = categories.CreateSelectListItem();
    }

    public async Task<IActionResult> OnGetGetDataTableAsync(ShowCategoryFeaturesViewModel categoryFeatures)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _categoryFeatureService.GetCategoryFeatures(categoryFeatures));
    }
}