using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;

namespace ProEShop.Services.Implements.Identity
{
    public class ApplicationUserManager
        : UserManager<User>, IApplicationUserManager
    {
        public ApplicationUserManager(
            IApplicationUserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<ApplicationUserManager> logger)
            : base(
                (UserStore<User, Role, ApplicationDbContext, long, UserClaim, UserRole, UserLogin, UserToken,
                    RoleClaim>) store,
                optionsAccessor, passwordHasher, userValidators, passwordValidators,
                keyNormalizer, errors, services, logger)
        {
        }

        #region CustomClass

        #endregion
    }
}