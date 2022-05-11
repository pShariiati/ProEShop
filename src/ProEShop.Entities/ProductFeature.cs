using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("ProductFeatures")]
[Index(nameof(ProductFeature.FeatureId), nameof(ProductFeature.ProductId), IsUnique = true)]
public class ProductFeature : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(2000)]
    public string Value { get; set; }

    public long ProductId { get; set; }

    public long FeatureId { get; set; }

    #endregion

    #region Relations

    public Product Product { get; set; }
    public Feature Feature { get; set; }

    #endregion
}