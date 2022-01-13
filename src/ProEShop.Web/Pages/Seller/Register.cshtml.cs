using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Seller;

public class RegisterModel : PageModel
{
    public RegisterSellerViewModel RegisterSeller { get; set; }

    public void OnGet()
    {
    }
}