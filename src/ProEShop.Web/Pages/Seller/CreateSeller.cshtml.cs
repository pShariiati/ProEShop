using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Seller;

public class CreateSellerModel : PageModel
{
    #region Constructor

    private readonly IApplicationUserManager _userManager;

    public CreateSellerModel(IApplicationUserManager userManager)
    {
        _userManager = userManager;
    }

    #endregion

    [BindProperty]
    public CreateSellerViewModel CreateSeller { get; set; }
        = new();

    public async Task<IActionResult> OnGet(string phoneNumber)
    {
        if (!await _userManager.CheckForUserIsSeller(phoneNumber))
        {
            return RedirectToPage("/Error");
        }

        CreateSeller.PhoneNumber = phoneNumber;
        return Page();
    }

    public void OnPost()
    {
        //await _signInManager.SignInAsync(user, true);
    }
}