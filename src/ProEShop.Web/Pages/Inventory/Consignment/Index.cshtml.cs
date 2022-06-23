using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Web.Pages.Inventory.Consignment;

public class IndexModel : InventoryPanelBase
{
    #region Constructor

    private readonly IConsignmentService _consignmentService;

    public IndexModel(IConsignmentService consignmentService)
    {
        _consignmentService = consignmentService;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowConsignmentsViewModel Consignments { get; set; }
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
        return Partial("List", await _consignmentService.GetConsignments(Consignments));
    }
}