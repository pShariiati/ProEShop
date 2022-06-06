using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEShop.Entities;

[Index(nameof(Guarantee.Title), IsUnique = true)]
[Table("Guarantees")]
public class Guarantee : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [NotMapped]
    public string FullTitle => $"گارانتی {MonthsCount} ماهه {Title}";

    public byte MonthsCount { get; set; }

    public bool IsConfirmed { get; set; }

    [Required]
    [MaxLength(50)]
    public string Picture { get; set; }

    #endregion

    #region Relations

    #endregion
}
