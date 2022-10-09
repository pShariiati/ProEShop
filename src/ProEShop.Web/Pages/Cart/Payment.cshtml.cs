using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Carts;

namespace ProEShop.Web.Pages.Cart;

[Authorize]
public class PaymentModel : PageModel
{
    #region Constructor

    private readonly ICartService _cartService;

    public PaymentModel(
        ICartService cartService,
        IAddressService addressService)
    {
        _cartService = cartService;
    }

    #endregion

    public PaymentViewModel PaymentPage { get; set; }
        = new();

    public async Task<IActionResult> OnGet()
    {
        var userId = User.Identity.GetLoggedInUserId();
        PaymentPage.CartItems = await _cartService.GetCartsForPaymentPage(userId);

        // اگر سبد خرید خالی بود، کاربر رو به صفحه سبد خرید انتقال بده
        if (PaymentPage.CartItems.Count < 1)
        {
            return RedirectToPage("Index");
        }

        return Page();
    }
}