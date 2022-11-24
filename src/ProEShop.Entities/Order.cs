using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Enums;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

[Table("Orders")]
[Index(nameof(OrderNumber), IsUnique = true)]
public class Order : EntityBase, IAuditableEntity
{
    #region Properties

    public long UserId { get; set; }

    /// <summary>
    /// شماره سفارش
    /// باید یونیک باشد
    /// این عدد از طریق
    /// TrackingNumber
    /// پرباد مقدار دهی میشود
    /// </summary>
    public long OrderNumber { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public long AddressId { get; set; }

    /// <summary>
    /// کد پیگیری بانک
    /// بعد از پرداخت وجه سفارش
    /// چرا از عدد استفاده نکرده ایم و نوع این پراپرتی از جنس رشته است ؟
    /// چون امکان دارد که در داخل کد پیگیری یکی از بانک ها حروف هم وجود داشته باشد
    /// </summary>
    [MaxLength(100)]
    public string BankTransactionCode { get; set; }

    /// <summary>
    /// آیا این سفارش توسط مقدار داخل کیف پول کاربر پرداخت شده است ؟
    /// اگر فالس باشد یعنی توسط درگاه اینترنتی پرداخت شده است
    /// اگر هم ترو باشد، یعنی توسط کیف پول پرداخت شده است
    /// </summary>
    public bool PayFromWallet { get; set; }

    public int TotalPrice { get; set; }

    public int? DiscountPrice { get; set; }

    public int FinalPrice { get; set; }

    public byte TotalScore { get; set; }

    public byte ShippingCount { get; set; }

    /// <summary>
    /// از کدام درگاه، پرداختی انجام شده است
    /// </summary>
    public PaymentGateway? PaymentGateway { get; set; }

    public OrderStatus Status { get; set; }

    public bool IsPay { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    public Address Address { get; set; }

    public ICollection<ParcelPost> ParcelPosts { get; set; }
        = new List<ParcelPost>();

    #endregion
}