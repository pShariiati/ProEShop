using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.Brands;

public class EditBrandViewMode
{
    [HiddenInput]
    public long Id { get; set; }

    [PageRemote(PageName = "Index", PageHandler = "CheckForTitleFaOnEdit",
        HttpMethod = "GET",
        ErrorMessage = AttributesErrorMessages.RemoteMessage,
        AdditionalFields = nameof(Id))]
    [Display(Name = "نام فارسی برند")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string TitleFa { get; set; }

    [PageRemote(PageName = "Index", PageHandler = "CheckForTitleEnOnEdit",
        HttpMethod = "GET",
        ErrorMessage = AttributesErrorMessages.RemoteMessage,
        AdditionalFields = nameof(Id))]
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
    [MaxFileSize(3)]
    public IFormFile NewLogoPicture { get; set; }

    [Display(Name = "برگه ثبت برند")]
    [IsImage]
    [MaxFileSize(3)]
    public IFormFile NewBrandRegistrationPicture { get; set; }

    [LtrDirection]
    [Display(Name = "لینک سایت قوه قضاییه")]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string JudiciaryLink { get; set; }

    [LtrDirection]
    [Display(Name = "لینک سایت معتبر خارجی")]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string BrandLinkEn { get; set; }

    [Display(Name = "لوگوی برند از قبل بارگذاری شده")]
    public string LogoPicture { get; set; }

    [Display(Name = "برگه ثبت برند از قبل بارگذاری شده")]
    public string BrandRegistrationPicture { get; set; }
}