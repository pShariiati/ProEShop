using System.ComponentModel.DataAnnotations;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.Sellers;

public class RegisterSellerViewModel
{
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    [EmailAddress(ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public string Email { get; set; }

    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [RegularExpression(@"^09[\d]{9}$", ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    [MaxLength(11, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string PhoneNumber { get; set; }

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(100, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "تکرار رمز عبور")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(100, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = AttributesErrorMessages.CompareMessage)]
    public string ConfirmPassword { get; set; }
}