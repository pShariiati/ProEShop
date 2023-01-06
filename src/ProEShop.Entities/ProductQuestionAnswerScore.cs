using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

/// <summary>
/// لایک و دیسلایک پاسخ های سوالات
/// هر کاربر میتواند برای هر جواب فقط یک لایک یا دیسلایک داشته باشد
/// </summary>
[Table("ProductsQuestionsAnswersScores")]
public class ProductQuestionAnswerScore
{
    #region Properties

    /// <summary>
    /// آیدی جواب سوال
    /// </summary>
    public long AnswerId { get; set; }

    /// <summary>
    /// آیدی کاربری که پاسخ داده
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// آیا لایک انجام شده یا دیسلایک
    /// </summary>
    public bool IsLike { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    public ProductQuestionAndAnswer Answer { get; set; }

    #endregion
}