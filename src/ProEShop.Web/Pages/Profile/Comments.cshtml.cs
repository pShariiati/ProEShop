using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.Profile;

public class CommentsModel : PageModel
{
    #region Constructor

    private readonly IParcelPostItemService _parcelPostItemService;

    public CommentsModel(IParcelPostItemService parcelPostItemService)
    {
        _parcelPostItemService = parcelPostItemService;
    }

    #endregion

    public ShowProductsInProfileCommentViewModel Products { get; set; }
        = new();

    public async Task OnGet()
    {
        Products = await _parcelPostItemService.GetProductsInProfileComment(Products);
    }
}