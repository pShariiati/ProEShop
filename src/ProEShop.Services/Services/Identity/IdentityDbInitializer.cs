using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProEShop.Common.GuardToolkit;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Identity.Settings;

namespace ProEShop.Services.Services.Identity;

public class IdentityDbInitializer : IIdentityDbInitializer
{
    private readonly IOptionsSnapshot<SiteSettings> _options;
    private readonly IApplicationUserManager _applicationUserManager;
    private readonly ILogger<IdentityDbInitializer> _logger;
    private readonly IApplicationRoleManager _roleManager;
    private readonly IServiceScopeFactory _scopeFactory;

    public IdentityDbInitializer(
        IApplicationUserManager applicationUserManager,
        IServiceScopeFactory scopeFactory,
        IApplicationRoleManager roleManager,
        IOptionsSnapshot<SiteSettings> adminUserSeedOptions,
        ILogger<IdentityDbInitializer> logger
    )
    {
        _applicationUserManager = applicationUserManager;
        _applicationUserManager.CheckArgumentIsNull(nameof(_applicationUserManager));

        _scopeFactory = scopeFactory;
        _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

        _roleManager = roleManager;
        _roleManager.CheckArgumentIsNull(nameof(_roleManager));

        _options = adminUserSeedOptions;
        _options.CheckArgumentIsNull(nameof(_options));

        _logger = logger;
        _logger.CheckArgumentIsNull(nameof(_logger));
    }

    /// <summary>
    /// Applies any pending migrations for the context to the database.
    /// Will create the database if it does not already exist.
    /// </summary>
    public void Initialize()
    {
        _scopeFactory.RunScopedService<ApplicationDbContext>(context =>
        {
            context.Database.Migrate();
        });
    }


    /// <summary>
    /// Adds some default values to the IdentityDb
    /// </summary>
    public void SeedData()
    {
        _scopeFactory.RunScopedService<IIdentityDbInitializer>(identityDbSeedData =>
        {
            var result = identityDbSeedData.SeedDatabaseWithAdminUserAsync().Result;
            if (result == IdentityResult.Failed())
            {
                throw new InvalidOperationException(result.DumpErrors());
            }
        });
    }

    public async Task<IdentityResult> SeedDatabaseWithAdminUserAsync()
    {
        var adminUserSeed = _options.Value.AdminUserSeed;
        adminUserSeed.CheckArgumentIsNull(nameof(adminUserSeed));

        var name = adminUserSeed.Username;
        var password = adminUserSeed.Password;
        var email = adminUserSeed.Email;

        var thisMethodName = nameof(SeedDatabaseWithAdminUserAsync);

        var adminUser = await _applicationUserManager.FindByNameAsync(name);
        if (adminUser != null)
        {
            _logger.LogInformation($"{thisMethodName}: adminUser already exists.");
            return IdentityResult.Success;
        }

        //Create the `Admin` Role if it does not exist
        var adminRole = await _roleManager.FindByNameAsync(ConstantRoles.Admin);
        if (adminRole == null)
        {
            adminRole = new Role(ConstantRoles.Admin, "ادمین کل سیستم");
            var adminRoleResult = await _roleManager.CreateAsync(adminRole);
            if (adminRoleResult == IdentityResult.Failed())
            {
                _logger.LogError($"{thisMethodName}: adminRole CreateAsync failed. {adminRoleResult.DumpErrors()}");
                return IdentityResult.Failed();
            }
        }
        else
        {
            _logger.LogInformation($"{thisMethodName}: adminRole already exists.");
        }

        adminUser = new User
        {
            UserName = name,
            Email = email,
            EmailConfirmed = true,
            Avatar = _options.Value.UserDefaultAvatar,
            SendSmsLastTime = DateTime.Now
        };
        var adminUserResult = await _applicationUserManager.CreateAsync(adminUser, password);
        if (adminUserResult == IdentityResult.Failed())
        {
            _logger.LogError($"{thisMethodName}: adminUser CreateAsync failed. {adminUserResult.DumpErrors()}");
            return IdentityResult.Failed();
        }

        var addToRoleResult = await _applicationUserManager.AddToRoleAsync(adminUser, adminRole.Name);
        if (addToRoleResult == IdentityResult.Failed())
        {
            _logger.LogError($"{thisMethodName}: adminUser AddToRoleAsync failed. {addToRoleResult.DumpErrors()}");
            return IdentityResult.Failed();
        }

        return IdentityResult.Success;
    }
}
