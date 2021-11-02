using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;

namespace ProEShop.Services.Implements.Identity;

public class ApplicationSignInManager
    : SignInManager<User>, IApplicationSignInManager
{
    //public ApplicationSignInManager(
    //    IApplicationUserManager userManager,
    //    IHttpContextAccessor contextAccessor,
    //    IUserClaimsPrincipalFactory<User> claimsFactory,
    //    IOptions<IdentityOptions> optionsAccessor,
    //    ILogger<ApplicationSignInManager> logger,
    //    IAuthenticationSchemeProvider schemes)
    //    : base((UserManager<User>)userManager,
    //        contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
    //{
    //}

    #region CustomClass
    #endregion
    public ApplicationSignInManager(
        IApplicationUserManager userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<User> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<ApplicationSignInManager> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<User> confirmation)
        : base((UserManager<User>)userManager,
            contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
    }
}
