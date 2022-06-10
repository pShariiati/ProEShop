using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Variants;

namespace ProEShop.Web.Pages.SellerPanel.Product;

public class AddVariantModel : PageModel
{
    #region Constructor

    private readonly IProductService _productService;

    public AddVariantModel(IProductService productService)
    {
        _productService = productService;
    }

    #endregion

    public AddVariantViewModel Variant { get; set; }

    public async Task<IActionResult> OnGet(long productId)
    {
        var productInfo = await _productService.GetProductInfoForAddVariant(productId);
        if (productInfo is null)
        {
            return RedirectToPage(PublicConstantStrings.Error404PageName);
        }

        Variant = productInfo;
        return Page();
    }
}