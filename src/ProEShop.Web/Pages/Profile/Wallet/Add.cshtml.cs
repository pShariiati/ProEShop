using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parbad;
using Parbad.AspNetCore;
using Parbad.Gateway.ZarinPal;
using ProEShop.Common.Constants;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Wallets;

namespace ProEShop.Web.Pages.Profile.Wallet;

public class AddModel : PageModel
{
    #region Constructor

    private readonly IWalletService _walletService;
    private readonly IOnlinePayment _onlinePayment;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AddModel(
        IWalletService walletService,
        IOnlinePayment onlinePayment,
        IUnitOfWork uow,
        IMapper mapper)
    {
        _walletService = walletService;
        _onlinePayment = onlinePayment;
        _uow = uow;
        _mapper = mapper;
    }

    #endregion

    [BindProperty]
    public AddValueToWalletViewModel AddValueToWallet { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // کاربر بعد از درگاه به چه آدرسی هدایت شود
        var callbackUrl = Url.PageLink("VerifyPayment", null, null, Request.Scheme);

        var walletToAdd = _mapper.Map<Entities.Wallet>(AddValueToWallet);
        walletToAdd.UserId = User.Identity.GetLoggedInUserId();
        walletToAdd.Description = "افزایش موجودی توسط کاربر";

        var result = await _onlinePayment.RequestAsync(invoice =>
        {
            invoice
                .SetAmount(AddValueToWallet.Value)
                .SetCallbackUrl(callbackUrl)
                .SetGateway(AddValueToWallet.PaymentGateway.ToString())
                .UseAutoIncrementTrackingNumber();
            if (AddValueToWallet.PaymentGateway == PaymentGateway.Zarinpal)
            {
                invoice.SetZarinPalData(new ZarinPalInvoice("No description"));
            }
        });

        walletToAdd.TrackingNumber = result.TrackingNumber;

        if (result.IsSucceed)
        {
            await _walletService.AddAsync(walletToAdd);
            await _uow.SaveChangesAsync();

            return result.GatewayTransporter.TransportToGateway();
        }
        else
        {
            return RedirectToPage(PublicConstantStrings.Error500PageName);
        }
    }
}