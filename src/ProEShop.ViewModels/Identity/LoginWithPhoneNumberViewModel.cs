using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.Identity;

public class LoginWithPhoneNumberViewModel
{
    [Display(Name = "کد فعال سازی")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [RegularExpression(@"[\d]{6}", ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    [MaxLength(6, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string ActivationCode { get; set; }
    
    [HiddenInput]
    public string PhoneNumber { get; set; }

    public byte SendSmsLastTimeMinute { get; set; }

    public byte SendSmsLastTimeSecond { get; set; }
}
