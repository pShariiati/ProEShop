using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("Features")]
public class Feature : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(150)]
    public string Title { get; set; }

    #endregion

    #region Relations

    public ICollection<CategoryFeature> CategoryFeatures { get; set; }

    #endregion
}