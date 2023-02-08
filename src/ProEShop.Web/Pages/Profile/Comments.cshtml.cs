using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.ProductComments;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.Profile;

public class CommentsModel : PageModel
{
    #region Constructor

    private readonly IParcelPostItemService _parcelPostItemService;
    private readonly IProductCommentService _productCommentService;

    public CommentsModel(
        IParcelPostItemService parcelPostItemService,
        IProductCommentService productCommentService)
    {
        _parcelPostItemService = parcelPostItemService;
        _productCommentService = productCommentService;
    }

    #endregion

    /// <summary>
    /// محصولات خریداری شده کاربر که در انتظار ارسال نظر هستند
    /// </summary>
    public ShowProductsInProfileCommentViewModel Products { get; set; }
        = new();

    /// <summary>
    /// کامنت های ارسال شده کاربر
    /// </summary>
    public ShowProductCommentsInProfile Comments { get; set; }
        = new();

    public bool IsActiveTabPending { get; set; }

    public async Task<IActionResult> OnGet(string activeTab = "pending")
    {
        if (activeTab != "pending" && activeTab != "comments")
        {
            return RedirectToPage(PublicConstantStrings.Error500PageName);
        }

        Products = await _parcelPostItemService.GetProductsInProfileComment(Products);
        Comments = await _productCommentService.GetCommentsInProfileComment(Comments);

        return Page();
    }
}