using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities;

namespace ProEShop.ViewModels.Sellers;

public class SellerDetailsViewModel
{
    [Display(Name = "شناسه")]
    public long Id { get; set; }

    [Display(Name = "نام فروشنده")]
    public string UserFullName { get; set; }

    [Display(Name = "شخص حقوقی / شخص حقیقی")]
    public bool IsRealPerson { get; set; }

    #region Legal person

    [Display(Name = "نام شرکت")]
    [MaxLength(200)]
    public string CompanyName { get; set; }

    [Display(Name = "شماره ثبت شرکت")]
    [MaxLength(100)]
    public string RegisterNumber { get; set; }

    [Display(Name = "کد اقتصادی")]
    [MaxLength(12)]
    public string EconomicCode { get; set; }

    [Display(Name = "نام افراد دارای حق امضا")]
    [MaxLength(300)]
    public string SignatureOwners { get; set; }

    [Display(Name = "شناسه ملی")]
    [MaxLength(30)]
    public string NationalId { get; set; }

    [Display(Name = "نوع شرکت")]
    public CompanyType? CompanyType { get; set; }
    #endregion

    [Display(Name = "کد فروشنده")]
    public int SellerCode { get; set; }

    [Display(Name = "نام فروشگاه")]
    [Required]
    [MaxLength(200)]
    public string ShopName { get; set; }

    [Display(Name = "درباره فروشگاه")]
    [Column(TypeName = "ntext")]
    public string AboutSeller { get; set; }

    [Display(Name = "لوگو فروشگاه")]
    [MaxLength(50)]
    public string Logo { get; set; }

    /// <summary>
    /// عکس کارت ملی
    /// </summary>
    [Display(Name = "تصویر کارت ملی")]
    [Required]
    [MaxLength(50)]
    public string IdCartPicture { get; set; }

    [Display(Name = "شماره شبا")]
    [Required]
    [MaxLength(24)]
    public string ShabaNumber { get; set; }

    [Display(Name = "شماره تلفن ثابت")]
    [Required]
    [MaxLength(11)]
    public string Telephone { get; set; }

    [Display(Name = "آدرس وبسایت")]
    [MaxLength(200)]
    public string Website { get; set; }

    [Display(Name = "استان")]
    public string ProvinceTitle { get; set; }

    [Display(Name = "شهرستان")]
    public string CityTitle { get; set; }

    [Display(Name = "آدرس کامل")]
    [Required]
    [MaxLength(300)]
    public string Address { get; set; }

    [Display(Name = "کد پستی")]
    [Required]
    [MaxLength(10)]
    public string PostalCode { get; set; }

    [MaxLength(100)]
    public string Location { get; set; }

    [Display(Name = "وضعیت مدارک")]
    public DocumentStatus DocumentStatus { get; set; }

    public bool IsActive { get; set; }

    [Display(Name = "تاریخ ثبت نام")]
    public DateTime CreatedDateTime { get; set; }
}