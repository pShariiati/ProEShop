using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Identity;
using ProEShop.ViewModels.Identity.Settings;

namespace ProEShop.Web.Pages.Identity;

public class RegisterLoginModel : PageBase
{
    #region Constructor

    private readonly IApplicationUserManager _userManager;
    private readonly ILogger<RegisterLoginModel> _logger;
    private readonly SiteSettings _siteSettings;
    private readonly ISmsSender _smsSender;
    private readonly IUnitOfWork _uow;
    private readonly IApplicationSignInManager _signInManager;

    public RegisterLoginModel(
        IApplicationUserManager userManager,
        ILogger<RegisterLoginModel> logger,
        IOptionsMonitor<SiteSettings> siteSettings,
        ISmsSender smsSender, IUnitOfWork uow,
        IApplicationSignInManager signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _siteSettings = siteSettings.CurrentValue;
        _smsSender = smsSender;
        _uow = uow;
        _signInManager = signInManager;
    }

    #endregion

    public RegisterLoginViewModel RegisterLogin { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(RegisterLoginViewModel registerLogin)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, PublicConstantStrings.ModelStateErrorMessage);
            return Page();
        }
        var isInputEmail = registerLogin.PhoneNumberOrEmail.IsEmail();
        if (!isInputEmail)
        {
            var addNewUser = false;
            var user = await _userManager.FindByNameAsync(registerLogin.PhoneNumberOrEmail);
            if (user is null)
            {
                user = new User
                {
                    UserName = registerLogin.PhoneNumberOrEmail,
                    PhoneNumber = registerLogin.PhoneNumberOrEmail,
                    Avatar = _siteSettings.UserDefaultAvatar,
                    Email = $"{StringHelpers.GenerateGuid()}@test.com"
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _uow.SaveChangesAsync();
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
                var phoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, registerLogin.PhoneNumberOrEmail);
                // todo: Send Sms token to the user
                //var sendSmsResult = await _smsSender.SendSmsAsync(user.PhoneNumber, $"کد فعال سازی شما\n {phoneNumberToken}");
                //if (!sendSmsResult)
                //{
                //    ModelState.AddModelError(string.Empty, "در ارسال پیامک خطایی به وجود آمد، لطفا دوباره سعی نمایید.");
                //    return Page();
                //}
                user.SendSmsLastTime = DateTime.Now;
                await _userManager.UpdateAsync(user);
                await _uow.SaveChangesAsync();
            }
        }
        return RedirectToPage("./LoginWithPhoneNumber", new { phoneNumber = registerLogin.PhoneNumberOrEmail });
    }

    public async Task<IActionResult> OnPostLogOut()
    {
        var user = User.Identity is { IsAuthenticated: true }
            ? await _userManager.FindByNameAsync(User.Identity.Name)
            : null;

        if (user != null)
        {
            await _userManager.UpdateSecurityStampAsync(user);
        }

        await _signInManager.SignOutAsync();

        return RedirectToPage("../Index");
    }
}