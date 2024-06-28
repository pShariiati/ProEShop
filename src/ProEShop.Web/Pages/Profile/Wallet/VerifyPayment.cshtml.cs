using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parbad;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;

namespace ProEShop.Web.Pages.Profile.Wallet;

[IgnoreAntiforgeryToken]
public class VerifyPaymentModel : PageModel
{
    private readonly IOnlinePayment _onlinePayment;
    private readonly IWalletService _walletService;
    private readonly IUnitOfWork _uow;

    public VerifyPaymentModel(
        IUnitOfWork uow,
        IWalletService walletService,
        IOnlinePayment onlinePayment)
    {
        _uow = uow;
        _walletService = walletService;
        _onlinePayment = onlinePayment;
    }

    public async Task<IActionResult> OnGet()
    {
        return await Verify();
    }

    public async Task<IActionResult> OnPost()
    {
        return await Verify();
    }

    /// <summary>
    /// تایید کردن پرداخت کاربر
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Verify()
    {
        var invoice = await _onlinePayment.FetchAsync();

        if (invoice.Status != PaymentFetchResultStatus.ReadyForVerifying)
        {
            // Check if the invoice is new or it's already processed before.
            var isAlreadyProcessed = invoice.Status == PaymentFetchResultStatus.AlreadyProcessed;
            var isAlreadyVerified = invoice.IsAlreadyVerified;
            return Content("The payment was not successful.");
        }

        var verifyResult = await _onlinePayment.VerifyAsync(invoice);

        // checking the status of the verification method
        if (verifyResult.Status != PaymentVerifyResultStatus.Succeed)
        {
            // checking if the payment is already verified
            var isAlreadyVerified = verifyResult.Status == PaymentVerifyResultStatus.AlreadyVerified;
            return Content("The payment is already verified before.");
        }

        var userId = User.Identity.GetLoggedInUserId();
        var wallet = await _walletService.FindByTrackingNumber(verifyResult.TrackingNumber, userId);
        if (wallet is null)
        {
            return Content("The payment was not successful.");
        }

        // شماره پیگیری بانک بعد از پرداخت وجه سفارش
        wallet.BankTransactionCode = verifyResult.TransactionCode;
        wallet.IsPay = true;
        await _uow.SaveChangesAsync();

        return Content("The payment was successful");
    }
}