using ProEShop.Entities.AuditableEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
    [MaxLength(200)]
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

    [ForeignKey(nameof(Category))]
    public long MainCategoryId { get; set; }

    [Column(TypeName = "ntext")]
    public string RejectReason { get; set; }

    public int ProductCode { get; set; }

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

    #endregion
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