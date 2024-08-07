﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

[Table("Sellers")]
[Index(nameof(Seller.ShabaNumber), IsUnique = true)]
[Index(nameof(Seller.ShopName), IsUnique = true)]
[Index(nameof(Seller.SellerCode), IsUnique = true)]
public class Seller : EntityBase, IAuditableEntity
{
    #region Properties

    public long UserId { get; set; }

    public bool IsRealPerson { get; set; }

    #region Legal person

    /// <summary>
    /// نام شرکت
    /// </summary>
    [MaxLength(200)]
    public string CompanyName { get; set; }

    /// <summary>
    /// شماره ثبت شرکت
    /// </summary>
    [MaxLength(100)]
    public string RegisterNumber { get; set; }

    /// <summary>
    /// کد اقتصادی
    /// </summary>
    [MaxLength(12)]
    public string EconomicCode { get; set; }

    /// <summary>
    /// نام افراد دارای حق امضا
    /// </summary>
    [MaxLength(300)]
    public string SignatureOwners { get; set; }

    /// <summary>
    /// شناسه ملی
    /// </summary>
    [MaxLength(30)]
    public string NationalId { get; set; }

    /// <summary>
    /// نوع شرکت
    /// </summary>
    public CompanyType? CompanyType { get; set; }
    #endregion

    /// <summary>
    /// کد فروشنده
    /// </summary>
    public int SellerCode { get; set; }

    /// <summary>
    /// نام فروشگاه
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string ShopName { get; set; }

    /// <summary>
    /// درباره فروشگاه
    /// </summary>
    [Column(TypeName = "ntext")]
    public string AboutSeller { get; set; }

    /// <summary>
    /// لوگو فروشگاه
    /// </summary>
    [MaxLength(50)]
    public string Logo { get; set; }

    /// <summary>
    /// عکس کارت ملی
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string IdCartPicture { get; set; }

    /// <summary>
    /// شماره شبا
    /// </summary>
    [Required]
    [MaxLength(24)]
    public string ShabaNumber { get; set; }

    /// <summary>
    /// شماره تلفن ثابت
    /// </summary>
    [Required]
    [MaxLength(11)]
    public string Telephone { get; set; }

    /// <summary>
    /// آدرس وبسایت
    /// </summary>
    [MaxLength(200)]
    public string Website { get; set; }

    /// <summary>
    /// استان
    /// </summary>
    public long ProvinceId { get; set; }

    /// <summary>
    /// شهرستان
    /// </summary>
    public long CityId { get; set; }

    /// <summary>
    /// آدرس کامل
    /// </summary>
    [Required]
    [MaxLength(300)]
    public string Address { get; set; }

    /// <summary>
    /// کد پستی
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string PostalCode { get; set; }

    [MaxLength(100)]
    public string Location { get; set; }

    /// <summary>
    /// وضعیت مدارک
    /// </summary>
    public DocumentStatus DocumentStatus { get; set; }

    public bool IsActive { get; set; } = true;

    /// <summary>
    /// تاریخ ثبت نام
    /// </summary>
    public DateTime CreatedDateTime { get; set; }

    /// <summary>
    /// دلایل رد مدراک فروشنده
    /// </summary>
    [Column(TypeName = "ntext")]
    public string RejectReason { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    public ProvinceAndCity Province { get; set; }

    public ProvinceAndCity City { get; set; }

    public ICollection<Brand> Brands { get; set; }

    public ICollection<ProductVariant> ProductVariants { get; set; }

    public ICollection<GiftCard> GiftCards { get; set; }

    #endregion
}

public enum DocumentStatus : byte
{
    [Display(Name = "در انتظار تایید اولیه")]
    AwaitingInitialApproval,

    [Display(Name = "تایید شده")]
    Confirmed,

    [Display(Name = "رد شده در حالت اولیه")]
    Rejected,

    [Display(Name = "در انتظار تایید فروشنده سیستم")]
    AwaitingApprovalSystemSeller,

    [Display(Name = "رد شده برای فروشنده  سیستم")]
    RejectedSystemSeller
}

public enum CompanyType : byte
{
    [Display(Name = "سهامی عام")]
    PublicStock,

    [Display(Name = "سهامی خاص")]
    PrivateEquity,

    [Display(Name = "مسئولیت محدود")]
    LimitedResponsibility,

    [Display(Name = "تعاونی")]
    Cooperative,

    [Display(Name = "تضامنی")]
    Solidarity
}