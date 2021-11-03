using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProEShop.Common.GuardToolkit;
using ProEShop.Common.PersianToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.ViewModels.Identity.Settings;

namespace ProEShop.IocConfig
{
    public static class DbContextOptionsExtensions
    {
        public static IServiceCollection AddConfiguredDbContext(
            this IServiceCollection services, SiteSettings siteSettings)
        {
            siteSettings.CheckArgumentIsNull(nameof(siteSettings));
            var connectionString = siteSettings.ConnectionStrings.ApplicationDbContextConnection;
            services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<ApplicationDbContext>());
            // We use `AddDbContextPool` instead of AddDbContext because it's faster
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(new PersianYeKeCommandInterceptor());
            });
            return services;
        }

        /// <summary>
        /// Creates and seeds the database.
        /// </summary>
        //public static void InitializeDb(this IServiceProvider serviceProvider)
        //{
        //    serviceProvider.RunScopedService<IIdentityDbInitializer>(identityDbInitialize =>
        //    {
        //        identityDbInitialize.Initialize();
        //        identityDbInitialize.SeedData();
        //    });
        //}
    }
}