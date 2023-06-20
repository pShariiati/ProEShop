using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Pages.Profile.Orders;

public class ReturnProductModel : PageModel
{
    #region Constructor

    private readonly IOrderService _orderService;

    public ReturnProductModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    #endregion

    public ReturnProductViewModel ReturnProduct { get; set; }

    public async Task OnGet(long orderNumber)
    {
        var userId = User.Identity.GetLoggedInUserId();
        ReturnProduct = await _orderService.GetOrderDetailsForReturnProduct(orderNumber, userId);
    }
}