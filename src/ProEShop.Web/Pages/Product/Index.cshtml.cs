using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.Product;

public class IndexModel : PageModel
{
    #region Constructor

    private readonly IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
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
}