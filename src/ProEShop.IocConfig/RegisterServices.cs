using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProEShop.DataLayer;

namespace ProEShop.IocConfig;
public static class RegisterServices
{
    public static IServiceCollection AddCustomeServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer("Server=.;Initial Catalog=ProEShop;Trusted_Connection=True;MultipleActiveResultSets=True");
        });
        return services;
    }
}
