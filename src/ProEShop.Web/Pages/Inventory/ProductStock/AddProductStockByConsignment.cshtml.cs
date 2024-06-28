using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.ProductStocks;

namespace ProEShop.Web.Pages.Inventory.ProductStock;

[CheckModelStateInRazorPages]
public class AddProductStockByConsignmentModel : InventoryPanelBase
{
    #region Constructor

    private readonly IConsignmentItemService _consignmentItemService;
    private readonly IConsignmentService _consignmentService;
    private readonly IProductStockService _productStockService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public AddProductStockByConsignmentModel(
        IConsignmentItemService consignmentItemService,
        IProductStockService productStockService,
        IMapper mapper,
        IUnitOfWork uow,
        IConsignmentService consignmentService)
    {
        _consignmentItemService = consignmentItemService;
        _productStockService = productStockService;
        _mapper = mapper;
        _uow = uow;
        _consignmentService = consignmentService;
    }

    #endregion

    [BindProperty]
    public AddProductStockByConsignmentViewModel AddProductStock { get; set; }

    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPost()
    {
        if (!await _consignmentService.CanAddStockForConsignmentItems(AddProductStock.ConsignmentId))
        {
            return Json(new JsonResultOperation(false,
                "موجودی این محموله قادر به افزایش و تغییر نمی باشد"));
        }

        if (!await _consignmentItemService.IsExistsByProductVariantIdAndConsignmentId(AddProductStock.ProductVariantId,
                AddProductStock.ConsignmentId))
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        var addOrUpdate = string.Empty;
        var productStock = await _productStockService.GetByProductVariantIdAndConsignmentId(AddProductStock.ProductVariantId, AddProductStock.ConsignmentId);
        if (productStock is null)
        {
            addOrUpdate = "افزایش";
            productStock = _mapper.Map<Entities.ProductStock>(AddProductStock);
            await _productStockService.AddAsync(productStock);
        }
        else
        {
            addOrUpdate = "ویرایش";
            productStock.Count = AddProductStock.Count;
        }
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true,
            $"موجودی محصول مورد نظر با موفقیت {addOrUpdate} یافت"));
    }
}