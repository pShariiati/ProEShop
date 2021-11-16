using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProEShop.Web.Pages;

[Authorize]
public class TestModel : PageModel
{
    public void OnGet()
    {
    }
}
