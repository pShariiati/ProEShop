using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

/// <summary>
/// گزارش کامنت ها
/// هر کاربر میتواند برای هر کامنت یک گزارش ثبت کند
/// </summary>
[Table("CommentsReports")]
public class CommentReport
{
    #region Properties

    public long ProductCommentId { get; set; }

    public long UserId { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    public ProductComment ProductComment { get; set; }

    #endregion
}