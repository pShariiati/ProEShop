using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Services.Contracts;

namespace ProEShop.Web.Pages.Product;

public class ProductShortLinkModel : PageModel
{
    #region Constructor

    private readonly IProductService _productService;

    public ProductShortLinkModel(IProductService productService)
    {
        _productService = productService;
    }

    #endregion

    public async Task<IActionResult> OnGet(string productShortLink)
    {
        if (!Regex.IsMatch(productShortLink, @"^[a-zA-Z0-9]{1,10}$"))
        {
            return RedirectToPage(PublicConstantStrings.Error404PageName);
        }

        var shortLinkInBytes = Encoding.UTF8.GetBytes(productShortLink);
        var shortLinkToCompare = string.Join(".", shortLinkInBytes);
        var product = await _productService.FindByShortLink(shortLinkToCompare);
        if (product.slug is null)
        {
            return RedirectToPage(PublicConstantStrings.Error404PageName);
        }

        return RedirectToPage("/Product/Index", new
        {
            product.slug,
            product.productCode
        });
    }
}