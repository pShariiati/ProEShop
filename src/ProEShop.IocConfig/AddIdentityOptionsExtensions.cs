using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProEShop.Common.GuardToolkit;
using ProEShop.Entities.Identity;
using ProEShop.Services.Services.Identity;
using ProEShop.ViewModels.Identity.Settings;

namespace ProEShop.IocConfig;

public static class AddIdentityOptionsExtensions
{
    public const string EmailConfirmationTokenProviderName = "ConfirmEmail";

    public static IServiceCollection AddIdentityOptions(
        this IServiceCollection services, SiteSettings siteSettings)
    {
        siteSettings.CheckArgumentIsNull(nameof(siteSettings));

        services.AddConfirmEmailDataProtectorTokenOptions(siteSettings);
        services.AddIdentity<User, Role>(identityOptions =>
            {
                SetPasswordOptions(identityOptions.Password, siteSettings);
                SetSignInOptions(identityOptions.SignIn, siteSettings);
                SetUserOptions(identityOptions.User);
                SetLockoutOptions(identityOptions.Lockout, siteSettings);
            }).AddUserStore<ApplicationUserStore>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleStore<ApplicationRoleStore>()
            .AddRoleManager<ApplicationRoleManager>()
            .AddSignInManager<ApplicationSignInManager>()
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            // You **cannot** use .AddEntityFrameworkStores() when you customize everything
            //.AddEntityFrameworkStores<ApplicationDbContext, long>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<ConfirmEmailDataProtectorTokenProvider<User>>(EmailConfirmationTokenProviderName);

        services.ConfigureApplicationCookie(identityOptionsCookies =>
        {
            var provider = services.BuildServiceProvider();
            SetApplicationCookieOptions(provider, identityOptionsCookies, siteSettings);
        });

        services.EnableImmediateLogout();

        return services;
    }

    private static void AddConfirmEmailDataProtectorTokenOptions(this IServiceCollection services,
        SiteSettings siteSettings)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.Tokens.EmailConfirmationTokenProvider = EmailConfirmationTokenProviderName;
        });

        services.Configure<ConfirmEmailDataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = siteSettings.EmailConfirmationTokenProviderLifespan;
        });
    }

    private static void EnableImmediateLogout(this IServiceCollection services)
    {
        services.Configure<SecurityStampValidatorOptions>(options =>
        {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
            options.OnRefreshingPrincipal = principalContext => { return Task.CompletedTask; };
        });
    }

    private static void SetApplicationCookieOptions(IServiceProvider provider,
        CookieAuthenticationOptions identityOptionsCookies, SiteSettings siteSettings)
    {
        identityOptionsCookies.Cookie.Name = siteSettings.CookieOptions.CookieName;
        identityOptionsCookies.Cookie.HttpOnly = true;
        identityOptionsCookies.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        identityOptionsCookies.Cookie.SameSite = SameSiteMode.Lax;
        // this cookie will always be stored regardless of the user's consent
        identityOptionsCookies.Cookie.IsEssential = true;

        identityOptionsCookies.ExpireTimeSpan = siteSettings.CookieOptions.ExpireTimeSpan;
        identityOptionsCookies.SlidingExpiration = siteSettings.CookieOptions.SlidingExpiration;
        identityOptionsCookies.LoginPath = siteSettings.CookieOptions.LoginPath;
        identityOptionsCookies.LogoutPath = siteSettings.CookieOptions.LogoutPath;
        identityOptionsCookies.AccessDeniedPath = siteSettings.CookieOptions.AccessDeniedPath;
    }

    private static void SetLockoutOptions(LockoutOptions identityOptionsLockout, SiteSettings siteSettings)
    {
        identityOptionsLockout.AllowedForNewUsers = siteSettings.LockoutOptions.AllowedForNewUsers;
        identityOptionsLockout.DefaultLockoutTimeSpan = siteSettings.LockoutOptions.DefaultLockoutTimeSpan;
        identityOptionsLockout.MaxFailedAccessAttempts = siteSettings.LockoutOptions.MaxFailedAccessAttempts;
    }

    private static void SetPasswordOptions(PasswordOptions identityOptionsPassword, SiteSettings siteSettings)
    {
        identityOptionsPassword.RequireDigit = siteSettings.PasswordOptions.RequireDigit;
        identityOptionsPassword.RequireLowercase = siteSettings.PasswordOptions.RequireLowercase;
        identityOptionsPassword.RequireNonAlphanumeric = siteSettings.PasswordOptions.RequireNonAlphanumeric;
        identityOptionsPassword.RequireUppercase = siteSettings.PasswordOptions.RequireUppercase;
        identityOptionsPassword.RequiredLength = siteSettings.PasswordOptions.RequiredLength;
    }

    private static void SetSignInOptions(SignInOptions identityOptionsSignIn, SiteSettings siteSettings)
    {
        identityOptionsSignIn.RequireConfirmedEmail = siteSettings.EnableEmailConfirmation;
    }

    private static void SetUserOptions(UserOptions identityOptionsUser)
    {
        identityOptionsUser.RequireUniqueEmail = true;
    }
}
