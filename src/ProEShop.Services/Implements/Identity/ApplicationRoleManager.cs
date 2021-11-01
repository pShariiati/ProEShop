using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;

namespace ProEShop.Services.Implements.Identity;
public class ApplicationRoleManager
    : RoleManager<Role>, IApplicationRoleManager
{
    public ApplicationRoleManager(
        IApplicationRoleStore store,
        IEnumerable<IRoleValidator<Role>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<ApplicationRoleManager> logger
        ) :
        base((RoleStore<Role, ApplicationDbContext, long, UserRole, RoleClaim>)store,
            roleValidators,keyNormalizer, errors, logger)
    {
    }

    #region CustomClass
    #endregion
}
