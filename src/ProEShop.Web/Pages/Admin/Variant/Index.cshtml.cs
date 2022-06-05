using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Variants;

namespace ProEShop.Web.Pages.Admin.Variant;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IVariantService _variantService;

    public IndexModel(
        IVariantService variantService)
    {
        _variantService = variantService;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowVariantsViewModel Variants { get; set; }
        = new();

    public void OnGet()
    {                                                                                                    

    }

    public async Task<IActionResult> OnGetGetDataTableAsync()
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _variantService.GetVariants(Variants));
    }
}