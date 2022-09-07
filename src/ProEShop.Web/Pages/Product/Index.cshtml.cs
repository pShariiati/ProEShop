using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.Product;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly IUserProductFavoriteService _userProductFavoriteService;
    private readonly IUnitOfWork _uow;
    private readonly IProductVariantService _productVariantService;
    private readonly ICartService _cartService;

    public IndexModel(
        IProductService productService,
        IUserProductFavoriteService userProductFavoriteService,
        IUnitOfWork uow,
        IProductVariantService productVariantService,
        ICartService cartService)
    {
        _productService = productService;
        _userProductFavoriteService = userProductFavoriteService;
        _uow = uow;
        _productVariantService = productVariantService;
        _cartService = cartService;
    }

    #endregion

    public ShowProductInfoViewModel ProductInfo { get; set; }

    public async Task<IActionResult> OnGet(int productCode, string slug)
    {
        ProductInfo = await _productService.GetProductInfo(productCode);
        if (ProductInfo is null)
        {
            return RedirectToPage(PublicConstantStrings.Error404PageName);
        }

        if (ProductInfo.Slug != slug)
        {
            return RedirectToPage("Index", new
            {
                productCode = productCode,
                slug = ProductInfo.Slug
            });
        }

        // آیدی های تنوع های این محصول
        var productVariantsIds = ProductInfo.ProductVariants.Select(x => x.Id).ToList();
        var userId = User.Identity.GetLoggedInUserId();
        // تنوع های این محصول که در سبد خرید این کاربری که، صفحه رو لود میکنه قرار داره
        ProductInfo.ProductVariantsInCart = await _cartService.GetProductVariantsInCart(productVariantsIds, userId);
        return Page();
    }

    public async Task<IActionResult> OnPostAddOrRemoveFavorite(long productId, bool addFavorite)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new JsonResultOperation(false));
        }

        if (!await _productService.IsExistsBy(nameof(Entities.Product.Id), productId))
        {
            return Json(new JsonResultOperation(false));
        }

        var userId = User.Identity.GetLoggedInUserId();

        var userProductFavorite = await _userProductFavoriteService.FindAsync(userId, productId);
        if (userProductFavorite is null && addFavorite)
        {
            await _userProductFavoriteService.AddAsync(new Entities.UserProductFavorite
            {
                ProductId = productId,
                UserId = userId
            });
        }
        else if (userProductFavorite != null && !addFavorite)
        {
            _userProductFavoriteService.Remove(userProductFavorite);
        }

        await _uow.SaveChangesAsync();

        return Json(new JsonResultOperation(true, string.Empty));
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

        // اگر کاونت سبد خرید برابر با مکس تعیین شده توسط فروشنده بود
        // یا مساوی تعداد موجودی داخل انبار، این متغیر ترو میشود
        var isCartFull = productVariant.MaxCountInCart == (cart?.Count ?? 1)
                         ||
                         (cart?.Count ?? 1) == productVariant.Count;


        return Json(new JsonResultOperation(true, "اوکیه")
        {
            Data = new
            {
                Count = cart?.Count ?? 1,
                ProductVariantId = productVariantId,
                IsCartFull = isCartFull
            }
        });
    }
}