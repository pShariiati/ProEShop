using Microsoft.AspNetCore.Identity;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities.Identity;

public class UserRole : IdentityUserRole<long>, IAuditableEntity
{
    public virtual User? User { get; set; }
    public virtual Role? Role { get; set; }
}
