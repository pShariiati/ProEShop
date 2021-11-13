using ProEShop.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.ViewModels.Identity;

public class RegisterLoginViewModel
{
    [Display(Name = "شماره تلفن / ایمیل")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [RegularExpression(@"^([\w-\.]+@([\w-]+\.)+[\w-]{2,})|09[\d]{9}$", ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    [MaxLength(150, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string PhoneNumberOrEmail { get; set; }
}
