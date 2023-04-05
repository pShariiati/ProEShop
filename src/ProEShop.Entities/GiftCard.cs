using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProEShop.Entities;

/// <summary>
/// موجودیت کارت هدیه
/// </summary>
[Table($"{nameof(GiftCard)}s")]
[Index(nameof(Code), IsUnique = true)]
public class GiftCard : EntityBase, IAuditableEntity
{
    #region Properties

    /// <summary>
    /// کد تخفیف
    /// </summary>
    [Required]
    [MaxLength(16)]
    public string Code { get; set; }

    /// <summary>
    /// مبلغ تخفیف
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// هزینه کل این سبد خرید، حداقل باید این مقدار باشد که کارت هدیه عمل کند
    /// برای مثال این مقدار 100 هزار تومان است
    /// حالا اگر قیمت کل سبد خرید کمتر از 100 هزار تومان باشد این کارت هدیه عمل نمیکند
    /// چرا ناله ؟
    /// چون امکان داره کارت هدیه برای هر قیمتی عمل کنه
    /// </summary>
    public int? MinimumPriceOfCart { get; set; }

    /// <summary>
    /// زمان پایان کارت هدیه
    /// در چه مواقعی مقدار میگیره ؟ برای مثال یک فروشنده یک کد تخفیف رو به همراه سفارش ارسال میکنه و میگه باید تا 3 روز دیگه
    /// باید از این کد تخفیف استفاده بشه وگرنه دیگه کار نمیکنه
    /// چرا ناله ؟ چون امکان داره تاریخ انقضا نداشته باشه، معمولا کارت های هدیه ایی که به فروش میرسند تاریخ انقضا ندارند
    /// </summary>
    public DateTime? EndDateTime { get; set; }

    /// <summary>
    /// در داخل سبد خرید باید یک محصول از این برند وجود داشته باشه که کارت هدیه عمل کنه
    /// چرا ناله ؟ چون امکان داره این کارت هدیه برای همه برند ها کار کنه
    /// </summary>
    public long? BrandId { get; set; }

    /// <summary>
    /// در داخل سبد خرید باید یک محصول از این دسته بندی وجود داشته باشه که کد کارت هدیه عمل کنه
    /// چرا ناله ؟ چون امکان داره این کد تخفیف برای همه دسته بندی ها کار کنه
    /// </summary>
    public long? CategoryId { get; set; }

    /// <summary>
    /// این کارت هدیه برای کدام فروشنده است
    /// فروشندگان میتوانند یک سری کارت هدیه داشته باشند که به میل خود برای خریداران به همراه سفارش  به صورت اشانتیون ارسال کنند
    /// چرا ناله ؟ چون امکان داره این کارت هدیه مال خود سایت باشه و مال فروشنده خاصی نباشه
    /// </summary>
    public long? SellerId { get; set; }

    #endregion

    #region Relations

    public Brand Brand { get; set; }

    public Category Category { get; set; }

    public Order Order { get; set; }

    public Seller Seller { get; set; }

    /// <summary>
    /// سفارشات تکمیل نشده ایی که از این کارت هدیه استفاده کرده اند
    /// </summary>
    public ICollection<Order> Orders { get; set; }

    #endregion
}