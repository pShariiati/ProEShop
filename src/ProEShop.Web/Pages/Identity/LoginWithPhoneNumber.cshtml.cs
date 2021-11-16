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

    public LoginWithPhoneNumberModel(IApplicationUserManager userManager)
    {
        _userManager = userManager;
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
}
