using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Pages.Profile.Orders;

/// <summary>
/// وارد کردن دلیل و جزییات مرجوعی کالا
/// </summary>
public class ReturnProductDetails : ProfilePageBase
{
    #region Constructor

    private readonly IReturnProductItemService _returnProductItemService;
    private readonly IReturnProductService _returnProductService;

    public ReturnProductDetails(
        IReturnProductItemService returnProductItemService,
        IReturnProductService returnProductService)
    {
        _returnProductItemService = returnProductItemService;
        _returnProductService = returnProductService;
    }

    #endregion

    public List<ReturnProductItemViewModel> ReturnProductItems { get; set; }

    public async Task<IActionResult> OnGet(long returnProductId)
    {
        if (!await _returnProductService.CheckForReturnProductDetails(returnProductId))
        {
            return NotFound();
        }
        ReturnProductItems = await _returnProductItemService.GetItemsByReturnProductId(returnProductId);
        return Page();
    }
}