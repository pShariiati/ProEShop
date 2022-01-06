using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Features;

namespace ProEShop.Web.Pages.Admin.Feature;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IFeatureService _categoryFeatureService;
    private readonly ICategoryService _categoryService;
    private readonly IUnitOfWork _uow;

    public IndexModel(
        IFeatureService categoryFeatureService,
        IUnitOfWork uow,
        ICategoryService categoryService)
    {
        _categoryFeatureService = categoryFeatureService;
        _uow = uow;
        _categoryService = categoryService;
    }

    #endregion

    public ShowFeaturesViewModel Features { get; set; }
        = new();

    public async Task OnGet()
    {
        var categories = await _categoryService.GetCategoriesToShowInSelectBoxAsync();
        Features.SearchFeatures.Categories = categories.CreateSelectListItem();
    }

    public async Task<IActionResult> OnGetGetDataTableAsync(ShowFeaturesViewModel features)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _categoryFeatureService.GetCategoryFeatures(features));
    }
}