using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.FeatureConstantValues;

namespace ProEShop.Web.Pages.Admin.FeatureConstantValue;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IFeatureConstantValueService _featureConstantValueService;

    public IndexModel(IFeatureConstantValueService featureConstantValueService)
    {
        _featureConstantValueService = featureConstantValueService;
    }

    #endregion

    public ShowFeatureConstantValuesViewModel FeatureConstantValues { get; set; }
        = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetGetDataTableAsync()
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, PublicConstantStrings.ModelStateErrorMessage);
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _featureConstantValueService.GetFeatureConstantValues(FeatureConstantValues));
    }
}