using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("CategoryFeatures")]
public class CategoryFeature : IAuditableEntity
{
    #region Properties

    public long CategoryId { get; set; }

    public long FeatureId { get; set; }

    #endregion

    #region Relations

    public Category Category { get; set; }
    public Feature Feature { get; set; }

    #endregion
}
