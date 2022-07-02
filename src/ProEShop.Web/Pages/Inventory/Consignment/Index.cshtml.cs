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
    private readonly IUnitOfWork _uow;
    private readonly ISellerService _sellerService;

    public IndexModel(
        IConsignmentService consignmentService,
        IUnitOfWork uow,
        ISellerService sellerService)
    {
        _consignmentService = consignmentService;
        _uow = uow;
        _sellerService = sellerService;
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

    public async Task<IActionResult> OnGetGetConsignmentDetails(long consignmentId)
    {
        if (consignmentId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var consignmentDetails = await _consignmentService.GetConsignmentDetails(consignmentId);
        if (consignmentDetails.Items.Count < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        return Partial("ConsignmentDetailsPartial", consignmentDetails);
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

    public async Task<IActionResult> OnGetAutocompleteSearch(string term)
    {
        return Json(await _sellerService.GetShopNamesForAutocomplete(term));
    }

    public async Task<IActionResult> OnPostReceiveConsignment(long consignmentId)
    {
        if (consignmentId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var consignment = await _consignmentService.GetConsignmentToChangeStatusToReceived(consignmentId);
        if (consignment is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        consignment.ConsignmentStatus = ConsignmentStatus.Received;
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true,
            "محموله مورد نظر با موفقیت دریافت شد، لطفا موجودی کالا ها را افزایش دهید"));
    }
}