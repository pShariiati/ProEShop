using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.ProductComments;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.Profile.Comments;

public class IndexModel : PageModel
{
    #region Constructor

    private readonly IParcelPostItemService _parcelPostItemService;
    private readonly IProductCommentService _productCommentService;

    public IndexModel(
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

        IsActiveTabPending = activeTab == "pending";

        if (IsActiveTabPending)
        {
            Products = await _parcelPostItemService.GetProductsInProfileComment(Products);
        }
        else
        {
            Comments = await _productCommentService.GetCommentsInProfileComment(Comments);
        }

        return Page();
    }

    /// <summary>
    /// نمایش نظرات به صورت صفحه بندی شده
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetShowCommentsByPagination(int pageNumber)
    {
        var comments = await _productCommentService.GetCommentsInProfileComment(pageNumber);
        return Partial("_Comments", comments);
    }

    /// <summary>
    /// نمایش محصولات به صورت صفحه بندی شده
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetShowProductsByPagination(int pageNumber)
    {
        var products = await _parcelPostItemService.GetProductsInProfileComment(pageNumber);
        return Partial("_Products", products);
    }
}