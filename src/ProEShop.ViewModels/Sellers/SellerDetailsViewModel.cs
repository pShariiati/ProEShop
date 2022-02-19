using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;
using ProEShop.Entities;
using ProEShop.Entities.Identity;

namespace ProEShop.ViewModels.Sellers;

public class SellerDetailsViewModel
{
    [Display(Name = "شناسه")]
    [HiddenInput]
    public long Id { get; set; }

    [Display(Name = "نام فروشنده")]
    public string UserFirstName { get; set; }

    [Display(Name = "نام خانوادگی فروشنده")]
    public string UserLastName { get; set; }

    public Gender UserGender { get; set; }

    public string UserFullName { get; set; }

    [Display(Name = "شخص حقوقی / شخص حقیقی")]
    public bool IsRealPerson { get; set; }

    #region Legal person

    [Display(Name = "نام شرکت")]
    public string CompanyName { get; set; }

    [Display(Name = "شماره ثبت شرکت")]
    public string RegisterNumber { get; set; }

    [Display(Name = "کد اقتصادی")]
    public string EconomicCode { get; set; }

    [Display(Name = "نام افراد دارای حق امضا")]
    public string SignatureOwners { get; set; }

    [Display(Name = "شناسه ملی")]
    public string NationalId { get; set; }

    [Display(Name = "نوع شرکت")]
    public CompanyType? CompanyType { get; set; }
    #endregion

    [Display(Name = "کد فروشنده")]
    public int SellerCode { get; set; }

    [Display(Name = "نام فروشگاه")]
    public string ShopName { get; set; }

    [Display(Name = "درباره فروشگاه")]
    public string AboutSeller { get; set; }

    [Display(Name = "لوگو فروشگاه")]
    public string Logo { get; set; }

    /// <summary>
    /// عکس کارت ملی
    /// </summary>
    [Display(Name = "تصویر کارت ملی")]
    public string IdCartPicture { get; set; }

    [Display(Name = "شماره شبا")]
    public string ShabaNumber { get; set; }

    [Display(Name = "شماره تلفن ثابت")]
    public string Telephone { get; set; }

    [Display(Name = "آدرس وبسایت")]
    public string Website { get; set; }

    [Display(Name = "استان")]
    public string ProvinceTitle { get; set; }

    [Display(Name = "شهرستان")]
    public string CityTitle { get; set; }

    [Display(Name = "آدرس کامل")]
    public string Address { get; set; }

    [Display(Name = "کد پستی")]
    public string PostalCode { get; set; }


    [Display(Name = "وضعیت مدارک")]
    public DocumentStatus DocumentStatus { get; set; }

    public bool IsActive { get; set; }

    [Display(Name = "تاریخ ثبت نام")]
    public string CreatedDateTime { get; set; }

    [MakeTinyMceRequired]
    [Display(Name = "دلایل رد مدراک فروشنده")]
    public string RejectReason { get; set; }
}