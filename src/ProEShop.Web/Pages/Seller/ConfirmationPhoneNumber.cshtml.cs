using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Seller;

public class ConfirmationPhoneNumberModel : PageBase
{
    #region Constructor

    private readonly IApplicationUserManager _userManager;
    private readonly IUnitOfWork _uow;

    public ConfirmationPhoneNumberModel(
        IApplicationUserManager userManager,
        IUnitOfWork uow)
    {
        _userManager = userManager;
        _uow = uow;
    }

    #endregion

    public ConfirmationSellerPhoneNumberViewModel Confirmation { get; set; }
        = new();

    [TempData]
    public string ActivationCode { get; set; }

    public async Task<IActionResult> OnGetAsync(string phoneNumber)
    {
        var userSendSmsLastTime = await _userManager.GetSendSmsLastTimeAsync(phoneNumber);
        if (userSendSmsLastTime is null)
        {
            return RedirectToPage("/Error");
        }
        #region Development
        var user = await _userManager.FindByNameAsync(phoneNumber);
        var phoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        ActivationCode = phoneNumberToken;
        #endregion
        var (min, sec) = userSendSmsLastTime.Value.GetMinuteAndSecondForLoginWithPhoneNumberPage();
        Confirmation.SendSmsLastTimeMinute = min;
        Confirmation.SendSmsLastTimeSecond = sec;
        Confirmation.PhoneNumber = phoneNumber;
        return Page();
    }

    public async Task<IActionResult> OnPostReSendSellerSmsActivationAsync(string phoneNumber)
    {
        //System.Threading.Thread.Sleep(2000);
        var user = await _userManager.FindByNameAsync(phoneNumber);
        if (user is null)
            return Json(new JsonResultOperation(false));
        if (user.SendSmsLastTime.AddMinutes(3) > DateTime.Now)
            return Json(new JsonResultOperation(false));
        var phoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        // todo: Send Sms token to the user
        //var sendSmsResult = await _smsSender.SendSmsAsync(user.PhoneNumber, $"کد فعال سازی شما\n {phoneNumberToken}");
        //if (!sendSmsResult)
        //{
        //    return Json(new JsonResultOperation(false, "در ارسال پیامک خطایی به وجود آمد، لطفا دوباره سعی نمایید"));
        //}
        user.SendSmsLastTime = DateTime.Now;
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "کد فعال سازی مجددا ارسال شد")
        {
            Data = new
            {
                activationCode = phoneNumberToken
            }
        });
    }
}