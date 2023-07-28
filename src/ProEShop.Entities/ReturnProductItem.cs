using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

/// <summary>
/// آیتم های داخل مرجوع کردن کالا
/// </summary>
[Table("ReturnProductItems")]
public class ReturnProductItem : EntityBase, IAuditableEntity
{
    #region Properties

    /// <summary>
    /// این آیتم متعلق به کدام مرجوعی کالا است
    /// </summary>
    public long ReturnProductId { get; set; }

    /// <summary>
    /// این آیتم، مرجوعی چه محصولی است
    /// </summary>
    public long ProductVariantId { get; set; }

    #endregion

    #region Relations

    public ReturnProduct ReturnProduct { get; set; }

    public ProductVariant ProductVariant { get; set; }

    #endregion
}