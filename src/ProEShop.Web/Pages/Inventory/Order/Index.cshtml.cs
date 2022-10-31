using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Pages.Inventory.Order;

public class IndexModel : InventoryPanelBase
{
    private readonly IOrderService _orderService;
    private readonly IProvinceAndCityService _provinceAndCityService;

    public IndexModel(
        IOrderService orderService,
        IProvinceAndCityService provinceAndCityService)
    {
        _orderService = orderService;
        _provinceAndCityService = provinceAndCityService;
    }

    [BindProperty(SupportsGet = true)]
    public ShowOrdersViewModel Orders { get; set; }
        = new();

    public async Task OnGet()
    {
        var provinces = await _provinceAndCityService.GetProvincesToShowInSelectBoxAsync();
        Orders.Provinces = provinces.CreateSelectListItem(firstItemValue: string.Empty);
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
        return Partial("List", await _orderService.GetOrders(Orders));
    }
}