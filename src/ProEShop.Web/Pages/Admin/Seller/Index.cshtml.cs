using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Admin.Seller;

public class IndexModel : PageModel
{
    public ShowSellersViewModel Sellers { get; set; }
        = new();

    public void OnGet()
    {
    }
}