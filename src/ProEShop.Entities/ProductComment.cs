using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

[Table("ProductComments")]
[Index(nameof(ProductComment.SellerId), nameof(ProductComment.ProductId),
    IsUnique = true)]
[Index(nameof(ProductComment.UserId), nameof(ProductComment.ProductId),
    IsUnique = true)]
public class ProductComment : EntityBase, IAuditableEntity
{
    #region Properties

    /// <summary>
    /// کدام کاربر نظر ثبت کرده است ؟
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// کدام فروشنده نظر ثبت کرده است ؟
    /// </summary>
    public long? SellerId { get; set; }

    public long ProductId { get; set; }

    public long? VariantId { get; set; }

    public byte Score { get; set; }

    [MaxLength(200)]
    public string CommentTitle { get; set; }

    [MaxLength(1000)]
    public string CommentText { get; set; }

    public bool? Suggest { get; set; }

    [MaxLength(1000)]
    public string PositiveItems { get; set; }

    [MaxLength(1000)]
    public string NegativeItems { get; set; }

    /// <summary>
    /// خریدار از کدام فروشنده خرید کرده است ؟
    /// </summary>
    public long? SellerShopNameId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public bool IsUnknown { get; set; }

    public bool IsBuyer { get; set; }

    /// <summary>
    /// اگر نال باشه یعنی نظر در حال بررسی است
    /// </summary>
    public bool? IsConfirmed { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    public Seller Seller { get; set; }

    public Product Product { get; set; }

    /// <summary>
    /// خریدار از کدام فروشنده خرید کرده است ؟
    /// </summary>
    public Seller SellerShopName { get; set; }

    public Variant Variant { get; set; }

    public ICollection<CommentScore> CommentsScores { get; set; }

    public ICollection<CommentReport> CommentsReports { get; set; }

    #endregion
}