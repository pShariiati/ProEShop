using System.ComponentModel.DataAnnotations;
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

    [MaxLength(200)]
    public string CompanyName { get; set; }

    [MaxLength(100)]
    public string RegisterNumber { get; set; }

    [MaxLength(12)]
    public string EconomicCode { get; set; }

    [MaxLength(300)]
    public string SignatureOwners { get; set; }

    [MaxLength(30)]
    public string NationalId { get; set; }

    public CompanyType? CompanyType { get; set; }
    #endregion

    public int SellerCode { get; set; }

    [Required]
    [MaxLength(200)]
    public string ShopName { get; set; }

    [Column(TypeName = "ntext")]
    public string AboutSeller { get; set; }

    [MaxLength(50)]
    public string Logo { get; set; }

    /// <summary>
    /// عکس کارت ملی
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string IdCartPicture { get; set; }

    [Required]
    [MaxLength(24)]
    public string ShabaNumber { get; set; }

    [Required]
    [MaxLength(11)]
    public string Telephone { get; set; }

    [MaxLength(200)]
    public string Website { get; set; }

    public long ProvinceId { get; set; }

    public long CityId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Address { get; set; }

    [Required]
    [MaxLength(10)]
    public string PostalCode { get; set; }
    
    [MaxLength(100)]
    public string Location { get; set; }

    public bool IsDocumentApproved { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDateTime { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    public ProvinceAndCity Province { get; set; }

    public ProvinceAndCity City { get; set; }

    #endregion
}

public enum CompanyType : byte
{
    [Display(Name = "سهمی عام")]
    PublicStock,

    [Display(Name = "سهامی خاص")]
    PrivatePerson,

    [Display(Name = "مسئولیت محدود")]
    LimitedResponsibility,

    [Display(Name = "تعاونی")]
    Cooperative,

    [Display(Name = "تضامنی")]
    Solidarity
}