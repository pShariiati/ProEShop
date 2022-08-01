using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("ProductShortLinks")]
public class ProductShortLink : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(10)]
    public string Link { get; set; }

    public bool IsUsed { get; set; }

    #endregion

    #region Relations

    public Product Product { get; set; }

    #endregion
}