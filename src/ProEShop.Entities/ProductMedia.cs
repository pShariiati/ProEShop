using System.ComponentModel.DataAnnotations;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

public class ProductMedia : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(50)]
    public string FileName { get; set; }

    public bool IsVideo { get; set; }

    public long ProductId { get; set; }

    #endregion

    #region Relations

    public Product Product { get; set; }

    #endregion
}