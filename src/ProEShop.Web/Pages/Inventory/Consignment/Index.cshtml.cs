using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Web.Pages.Inventory.Consignment;

public class IndexModel : InventoryPanelBase
{
    #region Constructor

    private readonly IConsignmentService _consignmentService;
    private readonly IConsignmentItemService _consignmentItemService;
    private readonly IUnitOfWork _uow;

    public IndexModel(
        IConsignmentService consignmentService,
        IConsignmentItemService consignmentItemService,
        IUnitOfWork uow)
    {
        _consignmentService = consignmentService;
        _consignmentItemService = consignmentItemService;
        _uow = uow;
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

    public async Task<IActionResult> OnGetGetConsignmentItems(long consignmentId)
    {
        if (consignmentId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var consignmentItems = await _consignmentItemService.GetConsignmentItems(consignmentId);
        if (consignmentItems.Count < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        return Partial("ConsignmentItems", consignmentItems);
    }

    public async Task<IActionResult> OnPostConfirmationConsignment(long consignmentId)
    {
        if (consignmentId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var consignment = await _consignmentService.GetConsignmentForConfirmation(consignmentId);
        if (consignment is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        consignment.ConsignmentStatus = ConsignmentStatus.ConfirmAndAwaitingForConsignment;
        await _uow.SaveChangesAsync();
        // Send email to the seller
        return Json(new JsonResultOperation(true,
            "محموله مورد نظر با موفقیت تایید شد و در انتظار دریافت توسط فروشنده قرار گرفت"));
    }
}