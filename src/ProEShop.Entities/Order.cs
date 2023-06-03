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

    public long? DiscountCodeId { get; set; }

    /// <summary>
    /// میزان کد تخفیف
    /// </summary>
    public int? DiscountCodePrice { get; set; }

    /// <summary>
    /// بعد از پرداخت اگه از کارت هدیه استفاده شده باشد این پراپرتی مقدار دهی میشود
    /// </summary>
    public long? GiftCardId { get; set; }

    /// <summary>
    /// میزان تخفیف کارت هدیه
    /// </summary>
    public int? GiftCardCodePrice { get; set; }

    /// <summary>
    /// هر کارت هدیه میتواند چندین سفارش تکمیل نشده داشته باشد
    /// به محض تکمیل سفارش
    /// GiftCardId
    /// مقدار دهی میشود و این پراپرتی نال میشود
    /// </summary>
    public long? ReservedGiftCardId { get; set; }

    /// <summary>
    /// این فیلد برای بررسی کردن امکان ثبت مرجوعی میباشد
    /// برای مثال این سفارش 3 مرسوله دارد
    /// مرسوله آخر در تاریخ بیستم به دست مشتری میرسد
    /// تا تاریخ بیست و هفتم باید دکمه ثبت مرجوعی رو به کاربر نمایش بدیم
    /// این فیلد توسط مسئول حمل و نقل زمانیکه وضعیت آخرین مرسوله این سفارش را به تحویل داده تغییر میدهد، مقدار دهی میشود
    /// اگر دکمه ثبت مرجوعی نمایان باشد یعنی حداقل یکی از مرسوله های این سفارش زمان مرجوع کردنش به پایان نرسیده است
    /// </summary>
    public DateTime? LastDeliveredParcelPostToClientDateTime { get; set; }

    #endregion

    #region Relations

    public GiftCard GiftCard { get; set; }

    public GiftCard ReservedGiftCard { get; set; }

    public User User { get; set; }

    public Address Address { get; set; }

    public ICollection<ParcelPost> ParcelPosts { get; set; }
        = new List<ParcelPost>();

    public ICollection<ParcelPostItem> ParcelPostItems { get; set; }
        = new List<ParcelPostItem>();

    public DiscountCode DiscountCode { get; set; }

    public ICollection<UsedDiscountCode> UsedDiscountCodes { get; set; }
        = new List<UsedDiscountCode>();

    #endregion
}