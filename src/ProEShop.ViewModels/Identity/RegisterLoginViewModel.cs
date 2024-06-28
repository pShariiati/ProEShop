using ProEShop.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.Identity;

public class RegisterLoginViewModel
{
    [Display(Name = "شماره تلفن / ایمیل")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [RegularExpression(@"^([\w-\.]+@([\w-]+\.)+[\w-]{2,})|09[\d]{9}$", ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string PhoneNumberOrEmail { get; set; }
}
