using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Identity.Settings;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Seller;

public class RegisterModel : PageModel
{
    #region Constructor

    private readonly IApplicationUserManager _userManager;
    private readonly SiteSettings _siteSettings;
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(
        IApplicationUserManager userManager,
        IOptionsMonitor<SiteSettings> siteSettings,
        ILogger<RegisterModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
        _siteSettings = siteSettings.CurrentValue;
    }

    #endregion

    [BindProperty]
    public RegisterSellerViewModel RegisterSeller { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, PublicConstantStrings.ModelStateErrorMessage);
            return Page();
        }

        var addNewUser = false;
        var user = await _userManager.FindByNameAsync(RegisterSeller.PhoneNumber);
        if (user is null)
        {
            user = new User
            {
                UserName = RegisterSeller.PhoneNumber,
                PhoneNumber = RegisterSeller.PhoneNumber,
                Avatar = _siteSettings.UserDefaultAvatar,
                Email = RegisterSeller.Email
            };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation(LogCodes.RegisterCode, $"{user.UserName} created a new account with phone number");
                addNewUser = true;
            }
            else
            {
                ModelState.AddErrorsFromResult(result);
                return Page();
            }
        }
        if (DateTime.Now > user.SendSmsLastTime.AddMinutes(3) || addNewUser)
        {
            var phoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, RegisterSeller.PhoneNumber);
            // todo: Send Sms token to the user
            //var sendSmsResult = await _smsSender.SendSmsAsync(user.PhoneNumber, $"کد فعال سازی شما\n {phoneNumberToken}");
            //if (!sendSmsResult)
            //{
            //    ModelState.AddModelError(string.Empty, "در ارسال پیامک خطایی به وجود آمد، لطفا دوباره سعی نمایید.");
            //    return Page();
            //}
            user.SendSmsLastTime = DateTime.Now;
            await _userManager.UpdateAsync(user);
        }

        return RedirectToPage("./ConfirmationPhoneNumber", new { phoneNumber = RegisterSeller.PhoneNumber });
    }
}