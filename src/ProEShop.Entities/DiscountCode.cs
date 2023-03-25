using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProEShop.Entities;

/// <summary>
/// موجودیت کد تخفیف
/// </summary>
[Table($"{nameof(DiscountCode)}s")]
[Index(nameof(Code), IsUnique = true)]
public class DiscountCode : EntityBase, IAuditableEntity
{
    #region Properties

    /// <summary>
    /// کد تخفیف
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Code { get; set; }

    /// <summary>
    /// تعداد باری که میشه ازش استفاده کرد
    /// چرا ناله ؟
    /// اگر نال باشه یعنی تعداد استفاده از این کد تخفیف بی نهایت است و تا زمان پایان
    /// میشه به هر تعداد از کد استفاده کرد
    /// </summary>
    public int? LimitedCount { get; set; }

    /// <summary>
    /// زمان شروع کد تخفیف
    /// </summary>
    public DateTime StartDateTime { get; set; }

    /// <summary>
    /// زمان پایان کد تخفیف
    /// </summary>
    public DateTime EndDateTime { get; set; }

    /// <summary>
    /// مبلغ تخفیف
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// هزینه کل این سبد خرید، حداقل باید این مقدار باشد که کد تخفیف عمل کند
    /// برای مثال این مقدار 100 هزار تومان است
    /// حالا اگر قیمت کل سبد خرید کمتر از 100 هزار تومان باشد این کد تخفیف عمل نمیکند
    /// چرا به صورت ناله ؟
    /// چون امکان داره کد تخفیف برای هر قیمتی عمل کنه
    /// </summary>
    public int? MinimumPriceOfCart { get; set; }

    /// <summary>
    /// در داخل سبد خرید باید یک محصول از این برند وجود داشته باشه که کد تخفیف عمل کنه
    /// چرا ناله ؟
    /// چون امکان داره این کد تخفیف برای همه برند ها کار کنه
    /// </summary>
    public long? BrandId { get; set; }

    /// <summary>
    /// در داخل سبد خرید باید یک محصول از این دسته بندی وجود داشته باشه که کد تخفیف عمل کنه
    /// چرا ناله ؟
    /// چون امکان داره این کد تخفیف برای همه دسته بندی ها کا ر کنه
    /// </summary>
    public long? CategoryId { get; set; }

    #endregion

    #region Relations

    public Brand Brand { get; set; }

    public Category Category { get; set; }

    public ICollection<UsedDiscountCode> UsedDiscountCodes { get; set; }

    #endregion
}