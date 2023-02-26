using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

/// <summary>
/// محصولات بازدید شده اخیر توسط کاربر
/// </summary>
[Table("UserHistories")]
public class UserHistory : IAuditableEntity
{
    #region Properties

    public long UserId { get; set; }

    public long ProductId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    public Product Product { get; set; }

    #endregion
}