using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Entities.Identity;

namespace ProEShop.Entities;

[Table("UserProductsFavorites")]
public class UserProductFavorite : IAuditableEntity
{
    #region Properties

    public long UserId { get; set; }

    public long ProductId { get; set; }

    #endregion

    #region Relations

    public User User { get; set; }
    public Product Product { get; set; }

    #endregion
}