using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Carts;

namespace ProEShop.Web.Pages.Cart;

public class IndexModel : PageModel
{
    #region Constructor

    private readonly ICartService _cartService;

    public IndexModel(ICartService cartService)
    {
        _cartService = cartService;
    }

    #endregion

    public List<ShowCartInCartPageViewModel> CartItems { get; set; }

    public async Task OnGet()
    {
        var userId = User.Identity.GetLoggedInUserId();
        CartItems = await _cartService.GetCartsForCartPage(userId);
    }
}