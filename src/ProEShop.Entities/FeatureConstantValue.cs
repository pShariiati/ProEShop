using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("FeatureConstantValue")]
public class FeatureConstantValue : EntityBase, IAuditableEntity
{
    #region Properties

    public long CategoryId { get; set; }

    public long FeatureId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Value { get; set; }

    #endregion

    #region Relations

    public Category Category { get; set; }

    public Feature Feature { get; set; }

    #endregion
}