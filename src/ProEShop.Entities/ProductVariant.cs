using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("ProductVariants")]
[Index(nameof(ProductVariant.SellerId),
    nameof(ProductVariant.ProductId),
    nameof(ProductVariant.VariantId),
    IsUnique = true)]
[Index(nameof(ProductVariant.VariantCode), IsUnique = true)]
public class ProductVariant : EntityBase, IAuditableEntity
{
    #region Properties

    public long ProductId { get; set; }

    /// <summary>
    /// بعضی از کالا مثل ماسک تنوع ندارن
    /// پس باید این پراپرتی نال باشه
    /// چون با هیج رکوردی از جدول تنوع در ارتباط نیست
    /// </summary>
    public long? VariantId { get; set; }

    public long GuaranteeId { get; set; }

    public long SellerId { get; set; }

    public int Price { get; set; }

    public int? OffPrice { get; set; }

    public byte? OffPercentage { get; set; }

    [NotMapped]
    public int FinalPrice => OffPrice ?? Price;

    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public int VariantCode { get; set; }

    public int Count { get; set; }

    /// <summary>
    /// فروشنده تعیین میکنه که در هر خرید چند آیتم از این تنوع در داخل سبد خرید اضافه شه
    /// </summary>
    public short MaxCountInCart { get; set; }

    #endregion

    #region Relations

    public Seller Seller { get; set; }

    public Product Product { get; set; }

    public Variant Variant { get; set; }

    public Guarantee Guarantee { get; set; }

    public ICollection<ConsignmentItem> ConsignmentItems { get; set; }

    public ICollection<ProductStock> ProductStocks { get; set; }

    public ICollection<ParcelPostItem> ParcelPostItems { get; set; }

    public ICollection<Cart> Carts { get; set; }

    #endregion
}