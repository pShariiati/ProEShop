using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("ConsignmentItems")]
[Index(nameof(ConsignmentItem.ConsignmentId),
    nameof(ConsignmentItem.ProductVariantId), IsUnique = true)]
public class ConsignmentItem : EntityBase, IAuditableEntity
{
    #region Properties

    public long ProductVariantId { get; set; }

    public long ConsignmentId { get; set; }

    public int Count { get; set; }
    
    [Required]
    [MaxLength(40)]
    // 4--1
    // long.MaxValue = 19 char
    // ProductVariantId = long (19 char) -- SellerId = long (19 char)
    // 19 + --(2) + 19 = 40 char
    public string Barcode { get; set; }

    #endregion

    #region Relations

    public ProductVariant ProductVariant { get; set; }

    public Consignment Consignment { get; set; }

    #endregion
}