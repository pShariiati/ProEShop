using Microsoft.AspNetCore.Identity;

namespace ProEShop.Entities.Identity
{
    public class RoleClaim : IdentityRoleClaim<long>
    {
        public virtual Role Role { get; set; }
    }
}
