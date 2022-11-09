using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.Services.Services;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Pages.Inventory.Order;

public class IndexModel : InventoryPanelBase
{
    private readonly IOrderService _orderService;
    private readonly IProvinceAndCityService _provinceAndCityService;
    private readonly IUnitOfWork _uow;

    public IndexModel(
        IOrderService orderService,
        IProvinceAndCityService provinceAndCityService,
        IUnitOfWork uow)
    {
        _orderService = orderService;
        _provinceAndCityService = provinceAndCityService;
        _uow = uow;
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

    public async Task<IActionResult> OnGetGetCities(long provinceId)
    {
        if (provinceId == 0)
        {
            return Json(new JsonResultOperation(true, string.Empty)
            {
                Data = new Dictionary<long, string>()
            });
        }

        if (provinceId < 1)
        {
            return Json(new JsonResultOperation(false, "استان مورد نظر را به درستی وارد نمایید"));
        }

        if (!await _provinceAndCityService.IsExistsBy(nameof(Entities.ProvinceAndCity.Id), provinceId))
        {
            return Json(new JsonResultOperation(false, "استان مورد نظر یافت نشد"));
        }

        var cities = await _provinceAndCityService.GetCitiesByProvinceIdInSelectBoxAsync(provinceId);
        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = cities
        });
    }

    public async Task<IActionResult> OnPostChangeStatusToInventoryProcessing(long orderId)
    {
        var order = await _orderService.FindByIdAsync(orderId);
        if (order is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        order.Status = OrderStatus.InventoryProcessing;

        await _uow.SaveChangesAsync();

        return Json(new JsonResultOperation(true, "سفارش مورد نظر وارد مرحله پردازش انبار شد"));
    }

    public async Task<IActionResult> OnGetGetOrderDetails(long orderId)
    {
        if (orderId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var consignmentDetails = await _orderService.GetOrderDetails(orderId);

        return Partial("_OrderDetailsPartial", consignmentDetails);
    }
}