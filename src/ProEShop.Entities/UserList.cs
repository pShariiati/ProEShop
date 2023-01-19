using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProEShop.Entities;

/// <summary>
/// لیست های محصولات کاربران
/// </summary>
[Table($"{nameof(UserList)}s")]
[Index(nameof(Title), nameof(UserId),
    IsUnique = true)]
public class UserList : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public long UserId { get; set; }

    public long UserListShortLinkId { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }

    public UserListShortLink UserListShortLink { get; set; }

    public ICollection<UserListProduct> UserListsProducts { get; set; }

    #endregion
}