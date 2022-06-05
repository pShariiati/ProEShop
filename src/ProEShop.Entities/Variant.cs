using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("Variants")]
[Index(nameof(Variant.Value), IsUnique = true)]
[Index(nameof(Variant.ColorCode), IsUnique = true)]
public class Variant : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(200)]
    public string Value { get; set; }

    public bool IsColor { get; set; }

    [MaxLength(7)]
    public string ColorCode { get; set; }

    public bool IsConfirmed { get; set; }

    #endregion

    #region Relations

    public ICollection<CategoryVariant> CategoryVariants { get; set; }

    #endregion
}