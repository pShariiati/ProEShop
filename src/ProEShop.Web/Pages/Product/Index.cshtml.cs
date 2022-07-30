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

    public IndexModel(
        IProductService productService,
        IUserProductFavoriteService userProductFavoriteService,
        IUnitOfWork uow)
    {
        _productService = productService;
        _userProductFavoriteService = userProductFavoriteService;
        _uow = uow;
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
}