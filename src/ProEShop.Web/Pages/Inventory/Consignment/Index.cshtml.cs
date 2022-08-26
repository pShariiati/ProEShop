using System.Text.Json.Serialization;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Web.Pages.Inventory.Consignment;

[CheckModelStateInRazorPages]
public class IndexModel : InventoryPanelBase
{
    #region Constructor

    private readonly IConsignmentService _consignmentService;
    private readonly IUnitOfWork _uow;
    private readonly ISellerService _sellerService;
    private readonly IHtmlSanitizer _htmlSanitizer;
    private readonly IProductStockService _productStockService;
    private readonly IProductVariantService _productVariantService;
    private readonly IProductService _productService;

    public IndexModel(
        IConsignmentService consignmentService,
        IUnitOfWork uow,
        ISellerService sellerService,
        IHtmlSanitizer htmlSanitizer,
        IProductStockService productStockService,
        IProductVariantService productVariantService,
        IProductService productService)
    {
        _consignmentService = consignmentService;
        _uow = uow;
        _sellerService = sellerService;
        _htmlSanitizer = htmlSanitizer;
        _productStockService = productStockService;
        _productVariantService = productVariantService;
        _productService = productService;
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

    public async Task<IActionResult> OnGetChangeConsignmentStatus(long consignmentId)
    {
        if (consignmentId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        if (!await _consignmentService.IsExistsConsignmentWithReceivedStatus(consignmentId))
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        var model = new AddDescriptionForConsignmentViewModel()
        {
            ConsignmentId = consignmentId
        };
        return Partial("ChangeConsignmentStatusToReceivedAndAddStockPartial", model);
    }

    public async Task<IActionResult> OnPostChangeConsignmentStatusToReceivedAndAddStock(AddDescriptionForConsignmentViewModel model)
    {
        var consignment = await _consignmentService.GetConsignmentWithReceivedStatus(model.ConsignmentId);
        if (consignment is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        consignment.ConsignmentStatus = ConsignmentStatus.ReceivedAndAddStock;
        consignment.Description = _htmlSanitizer.Sanitize(model.Description);

        #region AddStock

        var productStocks = await _productStockService.GetProductStocksForAddProductVariantsCount(model.ConsignmentId);
        var productVariantIds = productStocks.Select(x => x.Key).ToList();
        var productVariants = await _productVariantService.GetProductVariantsToAddCount(productVariantIds);

        foreach (var productStock in productStocks)
        {
            var productVariant = productVariants.SingleOrDefault(x => x.Id == productStock.Key);
            if (productVariant is not null)
            {
                productVariant.Count += productStock.Value;
            }
        }

        #endregion

        // وضعیت محصول هایی که در حالت ناموجود هستند
        // اونارو در حالت موجود قرار میدیم
        // چون موجودی اون محصولات
        // افزایش پیدا کرده

        // کد پایین مشکل داره
        // در جلسه آینده این مشکل رو حل میکنیم
        var product = await _productService.FindByIdAsync(productVariants.First().ProductId);
        if (product.ProductStockStatus == ProductStockStatus.Unavailable)
        {
            product.ProductStockStatus = ProductStockStatus.Available;
        }

        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true,
            "موجودی کالا ها با موفقیت افزایش یافت"
        ));
    }
}