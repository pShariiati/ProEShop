using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.Services.Services;
using ProEShop.ViewModels.Carts;

namespace ProEShop.Web.Pages.Cart;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly ICartService _cartService;
    private readonly IUnitOfWork _uow;
    private readonly IProductVariantService _productVariantService;
    private readonly IViewRendererService _viewRendererService;

    public IndexModel(
        ICartService cartService,
        IUnitOfWork uow,
        IProductVariantService productVariantService,
        IViewRendererService viewRendererService)
    {
        _cartService = cartService;
        _uow = uow;
        _productVariantService = productVariantService;
        _viewRendererService = viewRendererService;
    }

    #endregion

    public List<ShowCartInCartPageViewModel> CartItems { get; set; }

    public async Task OnGet()
    {
        var userId = User.Identity.GetLoggedInUserId();
        CartItems = await _cartService.GetCartsForCartPage(userId);
    }

    public async Task<IActionResult> OnPostAddProductVariantToCart(long productVariantId, bool isIncrease)
    {
        var productVariant = await _productVariantService.FindByIdAsync(productVariantId);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false));
        }

        var userId = User.Identity.GetLoggedInUserId();

        var cart = await _cartService.FindAsync(userId, productVariantId);
        if (cart is null)
        {
            var cartToAdd = new Entities.Cart()
            {
                ProductVariantId = productVariantId,
                UserId = userId,
                Count = 1
            };
            await _cartService.AddAsync(cartToAdd);
        }
        else if (isIncrease)
        {
            // فروشنده تعیین کرده که حداکثر تعدادی که کاربر طی هر خرید میتونه
            // از این محصول وارد سبد خرید کنه و خریدشو انجام بده 3 مورد است

            // مقدار داخل سبد خرید قبل فشردن دکمه به علاوه
            // 3
            cart.Count++;
            // بعد از زدن دکمه به علاوه
            // 4

            // چون تعداد داخل سبد خرید بیشتر از مقداری هست که فروشنده تعیین کرده
            // در نتیجه مقدار داخل سبد خرید رو به مقدار تعیین شده توسط فروشنده تغییر میدیم
            if (cart.Count > productVariant.MaxCountInCart)
                cart.Count = productVariant.MaxCountInCart;

            // موجودی انبار 2 عدد است
            // موجودی داخل سبد خرید هم 2 عدد است
            // حالا روی دکمه به علاوه کلیک میشه
            // چون حداکثر تعدادی که فروشنده تعیین کرده 3 است
            // در نتیجه از ایف بالا عبور میکنه و به ایف پایین میرسه
            // موقعی که روی دکمه به علاوه کلیک میشه
            // تعداد داخل سبد خرید میشه 3
            // و چون 3 بزرگتر از موجودی انبار یعنی 2 است
            // در نتیجه مقدار داخل سبد خرید هم به 2 تغییر میدیم

            // productVariant.Count => موجودی انبار برای این تنوع
            if (cart.Count > productVariant.Count)
                cart.Count = (short)productVariant.Count;
        }
        else
        {
            cart.Count--;
            if (cart.Count == 0)
            {
                _cartService.Remove(cart);
            }
        }

        await _uow.SaveChangesAsync();

        var carts = await _cartService.GetCartsForCartPage(userId);

        var cartBody = string.Empty;

        // اگر موردی داخل سبد خرید نبود، "پارشل سبد خرید خالی" رو نشون میدیم
        if (carts.Count == 0)
        {
            cartBody = await _viewRendererService.RenderViewToStringAsync("~/Pages/Cart/_EmptyCartPartial.cshtml");
        }
        else
        {
            cartBody = await _viewRendererService.RenderViewToStringAsync("~/Pages/Cart/_CartBodyPartial.cshtml", carts);
        }

        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = new
            {
                CartBody = cartBody
            }
        });
    }

    /// <summary>
    /// حذف همه موارد داخل سبد خرید
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> OnPostRemoveAllItemsInCart()
    {
        var userId = User.Identity.GetLoggedInUserId();
        var allItemsInCart = await _cartService.GetAllCartItems(userId);
        _cartService.RemoveRange(allItemsInCart);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = new
            {
                CartBody = await _viewRendererService.RenderViewToStringAsync("~/Pages/Cart/_EmptyCartPartial.cshtml")
            }
        });
    }
}