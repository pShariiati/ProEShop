using Microsoft.AspNetCore.Identity;

namespace ProEShop.Entities.Identity
{
    public class UserLogin : IdentityUserLogin<long>
    {
        public virtual User User { get; set; }
    }
}
