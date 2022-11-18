﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.Services.Services;
using ProEShop.ViewModels.Orders;
using ProEShop.ViewModels.ParcelPosts;

namespace ProEShop.Web.Pages.Inventory.DeliveryOrder;

[CheckModelStateInRazorPages]
public class IndexModel : InventoryPanelBase
{
    #region Constructor

    private readonly IOrderService _orderService;
    private readonly IProvinceAndCityService _provinceAndCityService;
    private readonly IUnitOfWork _uow;
    private readonly IParcelPostService _parcelPostService;

    public IndexModel(
        IOrderService orderService,
        IProvinceAndCityService provinceAndCityService,
        IUnitOfWork uow,
        IParcelPostService parcelPostService)
    {
        _orderService = orderService;
        _provinceAndCityService = provinceAndCityService;
        _uow = uow;
        _parcelPostService = parcelPostService;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowOrdersInDeliveryOrdersViewModel Orders { get; set; }
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
        return Partial("List", await _orderService.GetDeliveryOrders(Orders));
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

    public async Task<IActionResult> OnGetGetOrderDetails(long orderId)
    {
        if (orderId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var orderDetails = await _orderService.GetOrderDetails(orderId);

        if (orderDetails is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        return Partial("../Inventory/Order/_OrderDetailsPartial", orderDetails);
    }

    public async Task<IActionResult> OnGetShowDeliveryToPostPartial(long id)
    {
        if (!await _parcelPostService.IsExistsBy(nameof(Entities.ParcelPost.Id), id))
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        return Partial("_DeliveryToPostPartial");
    }

    public async Task<IActionResult> OnPostChangeStatusToDeliveryToPost(DeliveryParcelPostToPostViewModel model)
    {
        var parcelPost = await _parcelPostService.FindByIdAsync(model.Id);

        if (parcelPost is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        parcelPost.Status = ParcelPostStatus.DeliveredToPost;
        parcelPost.PostTrackingCode = model.PostTrackingCode;

        await _uow.SaveChangesAsync();

        return Json(
            new JsonResultOperation(true, "وضعیت مرسوله مورد نظر به \"تحویل داده شده به اداره پست\" تغییر یافت"));
    }
}