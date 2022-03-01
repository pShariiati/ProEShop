using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("Brands")]
public class Brand : EntityBase, IAuditableEntity
{
    #region Properties

    [Display(Name = "نام فارسی برند")]
    [Required]
    [MaxLength(200)]
    public string TitleFa { get; set; }

    [Display(Name = "نام انگلیسی برند")]
    [Required]
    [MaxLength(200)]
    public string TitleEn { get; set; }

    [Display(Name = "شرح برند")]
    [Required]
    [Column(TypeName = "ntext")]
    public string Description { get; set; }

    [Display(Name = "نوع برند")]
    public bool IsIranianBrand { get; set; }

    [Display(Name = "لوگوی برند")]
    [Required]
    [MaxLength(50)]
    public string LogoPicture { get; set; }

    [Display(Name = "برگه ثبت برند")]
    [MaxLength(50)]
    public string BrandRegistrationPicture { get; set; }

    [Display(Name = "لینک سایت قوه قضاییه")]
    [MaxLength(200)]
    public string JudiciaryLink { get; set; }

    [Display(Name = "لینک سایت معتبر خارجی")]
    [MaxLength(200)]
    public string BrandLinkEn { get; set; }

    #endregion
}