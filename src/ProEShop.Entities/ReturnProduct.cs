using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

/// <summary>
/// موجودیت مرجوع کردن کالا
/// </summary>
[Table("ReturnProducts")]
public class ReturnProduct : EntityBase, IAuditableEntity
{
    #region Properties

    /// <summary>
    /// شماره پیگیری این مرجوعی
    /// </summary>
    public long TrackingNumber { get; set; }

    /// <summary>
    /// این مرجوعی متعلق به کدام سفارش است
    /// </summary>
    public long OrderId { get; set; }

    #endregion

    #region Relations

    public Order Order { get; set; }

    public ICollection<ReturnProductItem> ReturnProductItems { get; set; }

    #endregion
}