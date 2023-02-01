using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.Services.Services;
using ProEShop.ViewModels;

namespace ProEShop.Web.ViewComponents;

public class MainHeaderViewComponent : ViewComponent
{
    #region Constructor

    private readonly ICartService _cartService;

    public MainHeaderViewComponent(ICartService cartService)
    {
        _cartService = cartService;
    }

    #endregion

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new MainHeaderViewModel();

        var userId = User.Identity.GetLoggedInUserId();
        model.Carts = await _cartService.GetCartsForDropDown(userId);
        model.AllProductsCountInCart = model.Carts.Sum(x => x.Count);

        return View(model);
    }
}