using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;

namespace ProEShop.Services.Services.Identity;

public class ApplicationRoleStore
    : RoleStore<Role, ApplicationDbContext, long, UserRole, RoleClaim>,
        IApplicationRoleStore
{
    public ApplicationRoleStore(
        IUnitOfWork uow,
        IdentityErrorDescriber describer = null)
        : base((ApplicationDbContext)uow, describer)
    {
    }

    #region CustomClass

    #endregion
}
