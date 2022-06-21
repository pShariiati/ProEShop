using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("ConsignmentItems")]
public class ConsignmentItem : EntityBase, IAuditableEntity
{
    #region Properties

    public long ProductVariantId { get; set; }

    public long ConsignmentId { get; set; }

    public int Count { get; set; }

    #endregion

    #region Relations

    public ProductVariant ProductVariant { get; set; }

    public Consignment Consignment { get; set; }

    #endregion
}