using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parbad;
using ProEShop.Common.Constants;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Pages.Cart;

[IgnoreAntiforgeryToken]
[Authorize]
public class VerifyPaymentModel : PageModel
{
    #region Constructor

    private readonly IOnlinePayment _onlinePayment;
    private readonly IOrderService _orderService;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public VerifyPaymentModel(
        IUnitOfWork uow,
        IOrderService orderService,
        IOnlinePayment onlinePayment,
        IMapper mapper)
    {
        _uow = uow;
        _orderService = orderService;
        _onlinePayment = onlinePayment;
        _mapper = mapper;
    }

    #endregion

    public VerifyPageDataViewModel VerifyPaymentData { get; set; }
        = new();

    public async Task<IActionResult> OnGet(long? orderNumber)
    {
        if (orderNumber != null)
        {
            var userId = User.Identity.GetLoggedInUserId();
            VerifyPaymentData = await _orderService.FindByOrderNumber(orderNumber.Value, userId);

            if (VerifyPaymentData is null)
            {
                return RedirectToPage(PublicConstantStrings.Error500PageName);
            }

            return Page();
        }

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
            return Page();
        }

        var verifyResult = await _onlinePayment.VerifyAsync(invoice);

        // checking the status of the verification method
        if (verifyResult.Status != PaymentVerifyResultStatus.Succeed)
        {
            // checking if the payment is already verified
            var isAlreadyVerified = verifyResult.Status == PaymentVerifyResultStatus.AlreadyVerified;
            return Page();
        }

        var userId = User.Identity.GetLoggedInUserId();
        var order = await _orderService.FindByOrderNumberAndIncludeParcelPosts(verifyResult.TrackingNumber, userId);
        if (order is null)
        {
            return RedirectToPage(PublicConstantStrings.Error500PageName);
        }

        // وضعیت مرسوله های این سفارش را به حالت "در حال پردازش" تغییر میدهیم
        foreach (var parcelPost in order.ParcelPosts)
        {
            parcelPost.Status = ParcelPostStatus.Processing;
        }

        // شماره پیگیری بانک بعد از پرداخت وجه سفارش
        order.BankTransactionCode = verifyResult.TransactionCode;
        order.Status = OrderStatus.Processing;
        order.IsPay = true;
        await _uow.SaveChangesAsync();

        VerifyPaymentData = _mapper.Map(order, VerifyPaymentData);

        return Page();
    }
}