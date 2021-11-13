using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.ViewModels.Identity;

namespace ProEShop.Web.Pages.Identity;

public class RegisterLoginModel : PageModel
{
    public RegisterLoginViewModel RegisterLogin { get; set; }
    public void OnGet()
    {
    }

    public IActionResult OnPost(RegisterLoginViewModel registerLogin)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, PublicConstantStrings.ModelStateErrorMessage);
            return Page();
        }
        return Page();
    }
}
