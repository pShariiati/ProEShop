using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

/// <summary>
/// پرسش های محصولات
/// </summary>
[Table("ProductsQuestionsAndAnswers")]
public class ProductQuestionAndAnswer : EntityBase, IAuditableEntity
{
    #region Properties

    /// <summary>
    /// متن سوال
    /// یا
    /// متن جواب
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Body { get; set; }

    /// <summary>
    /// آیا توسط ادمین تایید شده است ؟
    /// </summary>
    public bool IsConfirmed { get; set; }

    /// <summary>
    /// آیا کاربر میخواهد سوال یا جوابش به صورت ناشناس ثبت شود
    /// این پراپرتی برای تمامی سوالات ترو است
    /// و فقط برای جواب ها میتوان این مورد را ترو یا فالس کرد
    /// </summary>
    public bool IsUnknown { get; set; }

    /// <summary>
    /// آیا این فردی که جواب میدهد خریدار محصول است ؟
    /// این مورد چه ترو باشد چه فالس برای سوالات نمایش داده نمیشود
    /// و فقط برای پاسخ ها نمایش داده میشود
    /// </summary>
    public bool IsBuyer { get; set; }

    /// <summary>
    /// اگر سوال باشد این مورد نال است
    /// اگر پاسخ باشد آیدی سوال در این قسمت درج میشود
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// کدام فروشنده پاسخ داده است ؟
    /// فروشندگان قابلیت پرسش سوال را ندارند
    /// و اگر بخواهند پرسشی داشته باشند باید توسط یوزر خود، سوال بپرسند
    /// پس اگر این رکورد، سوال باشد این پراپرتی نال میشود
    /// </summary>
    public long? SellerId { get; set; }

    /// <summary>
    /// کدام کاربر این سوال یا پرسش را مطرح کرده است ؟
    /// اگر فروشگاه پاسخ ثبت کرده باشد این مورد نال میشود
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// برای کدام محصول سوال ثبت شده است ؟
    /// </summary>
    public long ProductId { get; set; }

    #endregion

    #region Relations

    public ProductQuestionAndAnswer Parent { get; set; }

    public ICollection<ProductQuestionAndAnswer> Answers { get; set; }

    public Seller Seller { get; set; }

    public User User { get; set; }

    public Product Product { get; set; }

    public ICollection<ProductQuestionAnswerScore> ProductsQuestionsAnswersScores { get; set; }

    #endregion
}