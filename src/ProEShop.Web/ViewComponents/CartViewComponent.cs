
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;

namespace ProEShop.Web.ViewComponents;

public class CartViewComponent : ViewComponent
{
    #region Constructor

    private readonly ICartService _cartService;

    public CartViewComponent(ICartService cartService)
    {
        _cartService = cartService;
    }

    #endregion

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = User.Identity.GetLoggedInUserId();
        var carts = await _cartService.GetCartsForDropDown(userId);
        return View("", carts);
    }
}