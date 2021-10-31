using Microsoft.AspNetCore.Identity;

namespace ProEShop.Entities.Identity
{
    public class UserToken : IdentityUserToken<long>
    {
        public virtual User User { get; set; }
    }
}
