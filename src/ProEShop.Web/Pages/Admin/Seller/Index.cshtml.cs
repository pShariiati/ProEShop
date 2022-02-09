using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Admin.Seller;

public class IndexModel : PageBase
{
    private readonly ISellerService _sellerService;

    public IndexModel(ISellerService sellerService)
    {
        _sellerService = sellerService;
    }

    [BindProperty]
    public ShowSellersViewModel Sellers { get; set; }
        = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetGetDataTableAsync()
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _sellerService.GetSellers(Sellers));
    }
}