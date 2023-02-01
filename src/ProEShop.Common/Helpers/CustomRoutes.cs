using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace ProEShop.Common.Helpers;

public static class CustomRoutes
{
    public static void CreateCustomRoutes(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages()
            .AddRazorPagesOptions(options =>
            {
                options.CreateCompareRoutes();
                options.CreateProductRoutes();
            });
    }

    public static void CreateCompareRoutes(this RazorPagesOptions options)
    {
        options.Conventions.AddPageRoute("/Compare/Index", "/compare/pc-{productCode1}");
        options.Conventions.AddPageRoute("/Compare/Index", "/compare/pc-{productCode1}/pc-{productCode2}");
        options.Conventions.AddPageRoute("/Compare/Index", "/compare/pc-{productCode1}/pc-{productCode2}/pc-{productCode3}");
        options.Conventions.AddPageRoute("/Compare/Index", "/compare/pc-{productCode1}/pc-{productCode2}/pc-{productCode3}/pc-{productCode4}");
    }

    public static void CreateProductRoutes(this RazorPagesOptions options)
    {
        options.Conventions.AddPageRoute("/Product/Index", "/Product/pc-{productCode}");
        options.Conventions.AddPageRoute("/Product/Index", "/Product/pc-{productCode}/{slug}");
    }
}