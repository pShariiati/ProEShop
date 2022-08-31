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
        if (!await _productVariantService.IsExistsBy(nameof(Entities.ProductVariant.Id), productVariantId))
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
        else
        {
            if (isIncrease)
                cart.Count++;
            else
                cart.Count--;
        }

        await _uow.SaveChangesAsync();

        return Json(new JsonResultOperation(true, "اوکیه")
        {
            Data = new
            {
                Count = cart?.Count ?? 1,
                ProductVariantId = productVariantId
            }
        });
    }
}