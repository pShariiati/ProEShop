using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;
using ProEShop.Entities;
using ProEShop.Entities.Identity;

namespace ProEShop.ViewModels.Sellers;

public class CreateSellerViewModel
{
    [Display(Name = "نام")]
    [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$",
        ErrorMessage = "لطفا تنها از حروف فارسی استفاده نمائید")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$",
        ErrorMessage = "لطفا تنها از حروف فارسی استفاده نمائید")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string LastName { get; set; }

    [Display(Name = "کد ملی")]
    [LtrDirection]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(10, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string NationalCode { get; set; }

    [Display(Name = "تاریخ تولد")]
    [LtrDirection]
    [RegularExpression(@"^۱۳[۰-۸][۰-۹]\/(۰[۱-۹]|۱[۰-۲])\/(۰[۱-۹]|[۱۲][۰-۹]|۳[۰۱])$", ErrorMessage = "سن باید بین ۱۸ و ۱۰۰ سال باشد")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    public string BirthDate { get; set; }
    
    public string BirthDateEn { get; set; }

    [Display(Name = "جنسیت")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    public Gender? Gender { get; set; }

    [Display(Name = "شماره تلفن")]
    [LtrDirection]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [HiddenInput]
    public string PhoneNumber { get; set; }

    public bool IsLegalPerson { get; set; }

    #region Legal person

    [Display(Name = "نام شرکت")]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string CompanyName { get; set; }

    [Display(Name = "شماره ثبت شرکت")]
    [MaxLength(100, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    [RegularExpression(@"^[\d]+$", ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public string RegisterNumber { get; set; }

    [Display(Name = "کد اقتصادی")]
    [LtrDirection]
    [MaxLength(12, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    [RegularExpression(@"^[\d]+$", ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public string EconomicCode { get; set; }

    [Display(Name = "نام افراد دارای حق امضا")]
    [MaxLength(300, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string SignatureOwners { get; set; }

    [Display(Name = "شناسه ملی")]
    [LtrDirection]
    [RegularExpression(@"^[\d]+$", ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    [MaxLength(30, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string NationalId { get; set; }

    [Display(Name = "نوع شرکت")]
    public CompanyType? CompanyType { get; set; }

    #endregion
    
    [Display(Name = "درباره فروشگاه")]
    public string AboutSeller { get; set; }

    [Display(Name = "لوگو فروشگاه")]
    [IsImage]
    [MaxFileSize(1)]
    public IFormFile LogoFile { get; set; }

    /// <summary>
    /// عکس کارت ملی
    /// </summary>
    [Display(Name = "تصویر کارت ملی")]
    [FileRequired]
    [IsImage]
    [MaxFileSize(1)]
    public IFormFile IdCartPictureFile { get; set; }

    [Display(Name = "شماره شبا")]
    [LtrDirection]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(24, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    [PageRemote(PageName = "/Seller/CreateSeller", PageHandler = "CheckForShabaNumber",
        HttpMethod = "GET",
        ErrorMessage = AttributesErrorMessages.RemoteMessage)]
    public string ShabaNumber { get; set; }

    [Display(Name = "شماره تلفن ثابت")]
    [LtrDirection]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(11, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    [RegularExpression(@"^0[\d]{10}$", ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public string Telephone { get; set; }

    [Display(Name = "آدرس وبسایت")]
    [LtrDirection]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string Website { get; set; }

    [Display(Name = "استان")]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    public long ProvinceId { get; set; }

    [Display(Name = "شهرستان")]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    public long CityId { get; set; }

    [Display(Name = "آدرس کامل")]
    [Required(ErrorMessage = "آدرس را وارد نمایید")]
    [MaxLength(300, ErrorMessage = "آدرس نباید بیشتر از {1} کاراکتر باشد")]
    public string Address { get; set; }
    
    [Display(Name = "کد پستی")]
    [LtrDirection]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(10, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    [RegularExpression(@"[\d]{10}", ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public string PostalCode { get; set; }

    public List<SelectListItem> Provinces { get; set; }

    [Display(Name = "قوانین و قرارداد را  به صورت کامل خوانده و قبول دارم")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "شما باید قوانین و مقرررات را تایید نمایید")]
    public bool AcceptToTheTerms { get; set; }
}
