using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Carts;

namespace ProEShop.Web.Pages.Cart;

[Authorize]
public class CheckoutModel : PageModel
{
    #region Constructor

    private readonly ICartService _cartService;
    private readonly IAddressService _addressService;

    public CheckoutModel(
        ICartService cartService,
        IAddressService addressService)
    {
        _cartService = cartService;
        _addressService = addressService;
    }

    #endregion

    public CheckoutViewModel CheckoutPage { get; set; }
        = new();

    public async Task<IActionResult> OnGet()
    {
        var userId = User.Identity.GetLoggedInUserId();
        CheckoutPage.CartItems = await _cartService.GetCartsForCheckoutPage(userId);

        // اگر سبد خرید خالی بود، کاربر رو به صفحه سبد خرید انتقال بده
        if (CheckoutPage.CartItems.Count < 1)
        {
            return RedirectToPage("Index");
        }
        CheckoutPage.UserAddress = await _addressService.GetAddressForCheckoutPage(userId);

        return Page();
    }
}