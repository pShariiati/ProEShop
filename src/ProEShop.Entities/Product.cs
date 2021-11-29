using ProEShop.Entities.AuditableEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEShop.Entities;

[Table("Products")]
public class Product : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(300)]
    public string Title { get; set; }

    public long CategoryId { get; set; }

    #endregion

    #region Relations

    public Category Category { get; set; }

    #endregion
}
