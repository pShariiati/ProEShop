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

    public CheckoutModel(ICartService cartService)
    {
        _cartService = cartService;
    }

    #endregion

    public List<ShowCartInCheckoutPageViewModel> CartItems { get; set; }

    public async Task OnGet()
    {
        var userId = User.Identity.GetLoggedInUserId();
        CartItems = await _cartService.GetCartsForCheckoutPage(userId);
    }
}