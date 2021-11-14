using Microsoft.AspNetCore.Identity;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities.Identity;

public class UserToken : IdentityUserToken<long>, IAuditableEntity
{
    public virtual User User { get; set; }
}
