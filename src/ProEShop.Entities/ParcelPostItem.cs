using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

/// <summary>
/// محصولات داخل مرسوله
/// </summary>
[Table("ParcelPostItems")]
public class ParcelPostItem : IAuditableEntity
{
    #region Properties

    public long ParcelPostId { get; set; }

    public long ProductVariantId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public long GuaranteeId { get; set; }

    public int Price { get; set; }

    public int? DiscountPrice { get; set; }

    public int Count { get; set; }

    public byte Score { get; set; }

    public long OrderId { get; set; }

    #endregion

    #region Relations

    public ParcelPost ParcelPost { get; set; }

    public ProductVariant ProductVariant { get; set; }

    public Guarantee Guarantee { get; set; }

    public Order Order { get; set; }

    #endregion
}