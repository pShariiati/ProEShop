using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProEShop.Common.GuardToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.IocConfig;
using ProEShop.ViewModels.Identity.Settings;

namespace ProEShop.IocConfig;

public static class IdentityServicesRegistry
{
    /// <summary>
    /// Adds all of the ASP.NET Core Identity related services and configurations at once.
    /// </summary>
    public static void AddCustomIdentityServices(this IServiceCollection services)
    {
        var siteSettings = GetSiteSettings(services);
        services.AddIdentityOptions(siteSettings);
        services.AddConfiguredDbContext(siteSettings);
        services.AddCustomServices();
    }

    public static SiteSettings GetSiteSettings(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        var siteSettingsOptions = provider.GetRequiredService<IOptionsSnapshot<SiteSettings>>();
        var siteSettings = siteSettingsOptions.Value;
        siteSettings.CheckArgumentIsNull(nameof(siteSettings));
        return siteSettings;
    }
}
