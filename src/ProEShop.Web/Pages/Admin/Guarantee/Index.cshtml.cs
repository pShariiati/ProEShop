using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Guarantees;
using ProEShop.ViewModels.Variants;

namespace ProEShop.Web.Pages.Admin.Guarantee;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IGuaranteeService _guaranteeService;

    public IndexModel(
        IGuaranteeService guaranteeService)
    {
        _guaranteeService = guaranteeService;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowGuaranteesViewModel Guarantees { get; set; }
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
        return Partial("List", await _guaranteeService.GetGuarantees(Guarantees));
    }
}