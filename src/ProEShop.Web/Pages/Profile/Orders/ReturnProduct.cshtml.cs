using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Pages.Profile.Orders;

public class ReturnProductModel : ProfilePageBase
{
    #region Constructor

    private readonly IOrderService _orderService;
    private readonly IParcelPostItemService _parcelPostItemService;
    private readonly IReturnProductService _returnProductService;
    private readonly IUnitOfWork _uow;

    public ReturnProductModel(
        IOrderService orderService,
        IParcelPostItemService parcelPostItemService,
        IReturnProductService returnProductService,
        IUnitOfWork uow)
    {
        _orderService = orderService;
        _parcelPostItemService = parcelPostItemService;
        _returnProductService = returnProductService;
        _uow = uow;
    }

    #endregion

    public ReturnProductViewModel ReturnProduct { get; set; }

    public async Task OnGet(long orderNumber)
    {
        var userId = User.Identity.GetLoggedInUserId();
        ReturnProduct = await _orderService.GetOrderDetailsForReturnProduct(orderNumber, userId);
    }

    public async Task<IActionResult> OnPost(long orderId, List<long> productVariantIdsToReturn)
    {
        var userId = User.Identity.GetLoggedInUserId();

        if (productVariantIdsToReturn.Count <= 0)
        {
            return JsonBadRequest();
        }

        if (!await _parcelPostItemService.CheckProductsVariantsForReturn(orderId, productVariantIdsToReturn, userId))
        {
            return JsonBadRequest();
        }

        var returnProductToAdd = new ReturnProduct()
        {
            OrderId = orderId,
            TrackingNumber = await _returnProductService.GetTrackingNumberForAddNewRecord(),
            Status = ReturnProductStatus.Draft
        };

        foreach (var productVariantId in productVariantIdsToReturn)
        {
            returnProductToAdd.ReturnProductItems.Add(new ReturnProductItem()
            {
                ProductVariantId = productVariantId
            });
        }

        await _returnProductService.AddAsync(returnProductToAdd);
        await _uow.SaveChangesAsync();

        return JsonOk("درخواست ثبت مرجوعی ثبت شد");
    }
}