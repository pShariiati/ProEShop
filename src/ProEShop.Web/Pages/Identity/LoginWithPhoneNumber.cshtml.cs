using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Helpers;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Identity;

namespace ProEShop.Web.Pages.Identity;

public class LoginWithPhoneNumberModel : PageModel
{
    #region Constructor

    private readonly IApplicationUserManager _userManager;
    private readonly IApplicationSignInManager _signInManager;

    public LoginWithPhoneNumberModel(
        IApplicationUserManager userManager,
        IApplicationSignInManager signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #endregion

    public LoginWithPhoneNumberViewModel LoginWithPhoneNumber { get; set; }
    = new LoginWithPhoneNumberViewModel();

    public async Task<IActionResult> OnGetAsync(string phoneNumber)
    {
        var userSendSmsLastTime = await _userManager.GetSendSmsLastTimeAsync(phoneNumber);
        if (userSendSmsLastTime is null)
        {
            return RedirectToPage("/Error");
        }

        var (min, sec) = userSendSmsLastTime.Value.GetMinuteAndSecondForLoginWithPhoneNumberPage();
        LoginWithPhoneNumber.SendSmsLastTimeMinute = min;
        LoginWithPhoneNumber.SendSmsLastTimeSecond = sec;
        LoginWithPhoneNumber.PhoneNumber = phoneNumber;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(LoginWithPhoneNumberViewModel loginWithPhoneNumber)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByNameAsync(loginWithPhoneNumber.PhoneNumber);
        if (user is null)
        {
            return Page();
        }

        var result = await _userManager.VerifyChangePhoneNumberTokenAsync(user, loginWithPhoneNumber.ActivationCode, loginWithPhoneNumber.PhoneNumber);
        if (!result)
        {
            return Page();
        }
        await _signInManager.SignInAsync(user, true);
        return RedirectToPage("/Test");
    }
}
