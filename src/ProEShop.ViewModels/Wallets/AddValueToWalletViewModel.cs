using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;
using ProEShop.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.ViewModels.Wallets;

/// <summary>
/// افزودن مقدار به کیف پول
/// </summary>
public class AddValueToWalletViewModel
{
    public PaymentGateway PaymentGateway { get; set; }

    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Range(10000, 20000000, ErrorMessage = "مبلغ وارد شده باید بین ۱۰/۰۰۰ تا ۲۰/۰۰۰/۰۰۰ تومان باشد")]
    [DivisibleBy10]
    public int Value { get; set; }
}