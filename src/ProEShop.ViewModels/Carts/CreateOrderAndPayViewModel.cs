using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;
using ProEShop.Entities.Enums;

namespace ProEShop.ViewModels.Carts;

/// <summary>
/// ویوو مدل صفحه ایجاد سفارش
/// </summary>
public class CreateOrderAndPayViewModel
{
    /// <summary>
    /// آیا پول سفارش از طریق کیف پول پرداخت شده است
    /// </summary>
    public bool PayFormWallet { get; set; }

    /// <summary>
    /// از کدام درگاه، پرداختی انجام شده است
    /// </summary>
    [Display(Name = "درگاه")]
    public PaymentGateway PaymentGateway { get; set; }

    [HiddenInput]
    [Display(Name = "کد تخفیف")]
    [MaxLength(100, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string DiscountCode { get; set; }
}