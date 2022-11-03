using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Enums;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

/// <summary>
/// مرسوله
/// هر سفارش میتواند شامل چندین مرسوله باشد
/// </summary>
[Table("ParcelPosts")]
[Index(nameof(PostTrackingCode), IsUnique = true)]
public class ParcelPost : EntityBase, IAuditableEntity
{
    #region Properties

    public long OrderId { get; set; }

    public Dimension Dimension { get; set; }

    public ParcelPostStatus Status { get; set; }

    /// <summary>
    /// کد رهگیری اداره پست
    /// </summary>
    [MaxLength(30)]
    public string PostTrackingCode { get; set; }

    /// <summary>
    /// هزینه پرداخت شده برای  پست کردن این مرسوله
    /// برای مثال در حال حاضر هزینه پستی 30 هزار است، اما بعد از شش
    /// ماه به 40 هزار تغییر میکند، ما باید بدونیم که 6 ماه پیش که این مرسوله ارسال
    /// شده است، چه هزینه ایی بابت پست پرداخت شده است
    /// </summary>
    public int ShippingPrice { get; set; }

    #endregion

    #region Relations

    public Order Order { get; set; }

    public ICollection<ParcelPostItem> ParcelPostItems { get; set; }
        = new List<ParcelPostItem>();

    #endregion
}