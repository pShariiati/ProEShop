using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Enums;

namespace ProEShop.Entities;

/// <summary>
/// موجودیت مرجوع کردن کالا
/// </summary>
[Table("ReturnProducts")]
[Index(nameof(TrackingNumber), IsUnique = true)]
public class ReturnProduct : EntityBase, IAuditableEntity
{
    #region Properties

    /// <summary>
    /// وضعیت این مرجوعی
    /// </summary>
    public ReturnProductStatus Status { get; set; }

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

    public ICollection<ReturnProductItem> ReturnProductItems { get; set; } = new List<ReturnProductItem>();

    #endregion
}