using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("Brands")]
[Index(nameof(TitleFa), IsUnique = true)]
[Index(nameof(TitleEn), IsUnique = true)]
[Index(nameof(Slug), IsUnique = true)]
public class Brand : EntityBase, IAuditableEntity
{
    #region Properties
    
    [Required]
    [MaxLength(200)]
    public string TitleFa { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string TitleEn { get; set; }

    public string FullTitle => $"{TitleFa} - {TitleEn}";
    
    [Required]
    [Column(TypeName = "ntext")]
    public string Description { get; set; }
    
    public bool IsIranianBrand { get; set; }
    
    [MaxLength(50)]
    public string LogoPicture { get; set; }
    
    [MaxLength(50)]
    public string BrandRegistrationPicture { get; set; }
    
    [MaxLength(200)]
    public string JudiciaryLink { get; set; }
    
    [MaxLength(200)]
    public string BrandLinkEn { get; set; }

    public bool IsConfirmed { get; set; }

    /// <summary>
    /// فروشنده پیشنهاد دهنده این برند
    /// </summary>
    public long? SellerId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Slug { get; set; }

    #endregion

    #region Relations

    public ICollection<CategoryBrand> CategoryBrands { get; set; }
        = new List<CategoryBrand>();

    public Seller Seller { get; set; }

    #endregion
}