using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProEShop.Common.Constants;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.Services.Implements.Identity;

public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
{
    public ApplicationClaimsPrincipalFactory(
        IApplicationUserManager userManager,
        IApplicationRoleManager roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base((UserManager<User>)userManager, (RoleManager<Role>)roleManager, optionsAccessor)
    {
    }

    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        // adds all `Options.ClaimsIdentity.RoleClaimType -> Role Claims` automatically +
        // `Options.ClaimsIdentity.UserIdClaimType -> userId`
        // & `Options.ClaimsIdentity.UserNameClaimType -> userName`
        var principal = await base.CreateAsync(user);
        AddCustomClaims(user, principal);
        return principal;
    }

    private static void AddCustomClaims(User user, IPrincipal principal)
    {
        (principal.Identity as ClaimsIdentity).AddClaims(new[]
        {
            new Claim(IdentityClaimNames.Avatar, user.Avatar ?? string.Empty, ClaimValueTypes.String),
            new Claim(IdentityClaimNames.FullName, user.FullName ?? string.Empty, ClaimValueTypes.String)
        });
    }
}
