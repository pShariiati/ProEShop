﻿using ProEShop.Entities.AuditableEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.Enums;

namespace ProEShop.Entities;

[Table("Products")]
[Index(nameof(Product.ProductCode), IsUnique = true)]
public class Product : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(200)]
    public string PersianTitle { get; set; }

    [MaxLength(200)]
    public string EnglishTitle { get; set; }

    [Required]
    [MaxLength(50)]
    public string Slug { get; set; }

    public bool IsFake { get; set; }

    public int PackWeight { get; set; }

    public int PackLength { get; set; }

    public int PackWidth { get; set; }

    public int PackHeight { get; set; }

    [Column(TypeName = "ntext")]
    public string ShortDescription { get; set; }

    [Column(TypeName = "ntext")]
    public string SpecialtyCheck { get; set; }

    public long SellerId { get; set; }

    public long BrandId { get; set; }

    public ProductStatus Status { get; set; }

    public ProductStockStatus ProductStockStatus { get; set; }

    [ForeignKey(nameof(Category))]
    public long MainCategoryId { get; set; }

    [Column(TypeName = "ntext")]
    public string RejectReason { get; set; }

    public int ProductCode { get; set; }

    public long ProductShortLinkId { get; set; }

    /// <summary>
    /// ابعاد
    /// </summary>
    public Dimension Dimension { get; set; }

    /// <summary>
    /// قیمت این محصول
    /// هر کدام از تنوع ها ارزون تر باشن قیمتش اینجا درج میشه
    /// الان دیگه نیازی نیست برای گرفتن قیمت این محصول به جدول تنوع ها رجوع کنیم
    /// چرا ناله ؟ چون موقعی که محصول تازه ایجاد شده و یا موجودیش به اتمام رسیده این محصول دیگه قیمت نداره
    /// چگونه مقدار دهی میشه ؟
    /// موقعی که یک فروشنده موجودی این محصول رو افزایش میده، باید بررسی کنیم که از میان تمام تنوع های این محصول کدوم
    /// یکی از همه ارزونتره و قیمت ارزانترین تنوع رو اینجا درج کنیم
    /// </summary>
    public int? Price { get; set; }

    /// <summary>
    /// تعداد مشاهده
    /// </summary>
    public long VisitCount { get; set; }

    /// <summary>
    /// تعداد فروش از این محصول
    /// </summary>
    public long SaleCount { get; set; }

    #endregion

    #region Relations

    public ICollection<ProductMedia> ProductMedia { get; set; }
        = new List<ProductMedia>();

    public ICollection<ProductCategory> ProductCategories { get; set; }
        = new List<ProductCategory>();

    public ICollection<ProductFeature> ProductFeatures { get; set; }
        = new List<ProductFeature>();

    public Brand Brand { get; set; }

    public Seller Seller { get; set; }

    public Category Category { get; set; }

    public ICollection<ProductVariant> ProductVariants { get; set; }

    public ICollection<ProductComment> ProductComments { get; set; }

    public ICollection<UserProductFavorite> UserProductsFavorites { get; set; }

    public ICollection<ProductQuestionAndAnswer> ProductsQuestionsAndAnswers { get; set; }

    public ProductShortLink ProductShortLink { get; set; }

    public ICollection<DiscountNotice> DiscountNotices { get; set; }

    public ICollection<UserListProduct> UserListsProducts { get; set; }

    public ICollection<UserHistory> UserHistories { get; set; }

    #endregion
}

public enum ProductStockStatus : byte
{
    /// <summary>
    /// همینکه محصول ایجاد بشه
    /// وضعیت محصول در حالت جدید قرار میگیره
    /// </summary>
    [Display(Name = "جدید")]
    New,

    /// <summary>
    /// اگر انباردار موجودی رو افزایش بده
    /// و وضعیت محصول در حالت نا موجود باشه
    /// وضعیت موجودی محصول رو به حالت موجود تغییر میدیم
    /// </summary>
    [Display(Name = "موجود")]
    Available,

    /// <summary>
    /// اگر محصول جدید باشد و یک فروشنده تنوعی برای آن اضافه کند
    /// وضعیت آن از حالت جدید به حالت نا موجود تغییر میکند
    /// </summary>
    [Display(Name = "ناموجود")]
    Unavailable
}

public enum ProductStatus : byte
{
    [Display(Name = "در انتظار تایید اولیه")]
    AwaitingInitialApproval,

    [Display(Name = "تایید شده")]
    Confirmed,

    [Display(Name = "رد شده در حالت اولیه")]
    Rejected
}