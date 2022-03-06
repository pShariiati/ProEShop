using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.Brands;

public class AddBrandViewModel
{
    [Display(Name = "نام فارسی برند")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string TitleFa { get; set; }

    [Display(Name = "نام انگلیسی برند")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string TitleEn { get; set; }

    [Display(Name = "شرح برند")]
    [MakeTinyMceRequired]
    public string Description { get; set; }

    [Display(Name = "نوع برند")]
    public bool IsIranianBrand { get; set; }

    [Display(Name = "لوگوی برند")]
    [IsImage]
    [FileRequired]
    [MaxFileSize(3)]
    public IFormFile LogoPicture { get; set; }

    [Display(Name = "برگه ثبت برند")]
    [IsImage]
    [MaxFileSize(3)]
    public IFormFile BrandRegistrationPicture { get; set; }

    [Display(Name = "لینک سایت قوه قضاییه")]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string JudiciaryLink { get; set; }

    [Display(Name = "لینک سایت معتبر خارجی")]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string BrandLinkEn { get; set; }
}