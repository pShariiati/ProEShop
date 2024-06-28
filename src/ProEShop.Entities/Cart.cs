using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

[Table("Carts")]
public class Cart : IAuditableEntity
{
    #region Properties

    public long UserId { get; set; }

    public long ProductVariantId { get; set; }

    public short Count { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    public ProductVariant ProductVariant { get; set; }

    #endregion
}