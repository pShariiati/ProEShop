using Microsoft.AspNetCore.Identity;

namespace ProEShop.Entities.Identity
{
    public class UserClaim : IdentityUserClaim<long>
    {
        public virtual User User { get; set; }
    }
}
