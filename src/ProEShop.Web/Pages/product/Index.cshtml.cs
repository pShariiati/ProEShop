using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProEShop.Web.Pages.Product;

public class IndexModel : PageModel
{
    public void OnGet(int productCode, string slug)
    {
        ViewData["test1"] = slug;
        ViewData["test2"] = productCode;
    }
}