using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    private readonly IUnitOfWork _uow;

    public IndexModel(ICategoryFeatureService categoryFeatureService, IUnitOfWork uow)
    {
        _categoryFeatureService = categoryFeatureService;
        _uow = uow;
    }

    #endregion

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetGetDataTableAsync(ShowCategoryFeaturesViewModel categoryFeature)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _categoryFeatureService.GetCategoryFeatures(categoryFeature));
    }
}