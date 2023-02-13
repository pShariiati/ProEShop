using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Enums;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

[Table($"{nameof(Wallet)}s")]
[Index(nameof(TrackingNumber), IsUnique = true)]
public class Wallet : EntityBase, IAuditableEntity
{
    #region Properties

    public long UserId { get; set; }

    /// <summary>
    /// چه مقداری کم یا اضافه شده است
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// آیا این مقدار از کیف پول کم شده یا اضافه شده است
    /// </summary>
    public bool IsAdd { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    /// <summary>
    /// از کدام درگاه، پرداختی انجام شده است
    /// </summary>
    public PaymentGateway PaymentGateway { get; set; }

    /// <summary>
    /// آیا پرداخت تکمیل شده است ؟
    /// </summary>
    public bool IsPay { get; set; }

    /// <summary>
    /// کد پیگیری بانک
    /// بعد از پرداخت وجه سفارش
    /// چرا از عدد استفاده نکرده ایم و نوع این پراپرتی از جنس رشته است ؟
    /// چون امکان دارد که در داخل کد پیگیری یکی از بانک ها حروف هم وجود داشته باشد
    /// </summary>
    [MaxLength(100)]
    public string BankTransactionCode { get; set; }

    /// <summary>
    /// شماره پیگیری
    /// توسط پرباد مقدار دهی میشود
    /// و در حین تایید کردن تراکنش، از این شماره برای استفاده میشود
    /// </summary>
    public long TrackingNumber { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    #endregion
}