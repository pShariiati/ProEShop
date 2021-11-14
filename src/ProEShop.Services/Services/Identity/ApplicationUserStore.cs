using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;

namespace ProEShop.Services.Services.Identity;

public class ApplicationUserStore
    : UserStore<User, Role, ApplicationDbContext, long, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>,
        IApplicationUserStore
{
    public ApplicationUserStore(
        IUnitOfWork uow,
        IdentityErrorDescriber describer = null)
        : base((ApplicationDbContext)uow, describer)
    {
    }

    #region CustomClass

    #endregion
}
