using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

/// <summary>
/// لایک و دیسلایک کامنت ها
/// هر کاربر میتواند برای هر کامنت فقط یک لایک یا دیسلایک داشته باشد
/// </summary>
[Table("CommentsScores")]
public class CommentScore
{
    #region Properties

    public long ProductCommentId { get; set; }

    public long UserId { get; set; }

    public bool IsLike { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    public ProductComment ProductComment { get; set; }

    #endregion
}