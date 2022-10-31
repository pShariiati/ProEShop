using System.Reflection;
using DNTCommon.Web.Core;
using Microsoft.Extensions.WebEncoders;
using ProEShop.IocConfig;
using ProEShop.ViewModels.Identity.Settings;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parbad.Builder;
using Parbad.Gateway.Mellat;
using Parbad.Storage.EntityFrameworkCore.Builder;
using ProEShop.DataLayer.Context;
using Parbad.Gateway.ZarinPal;
using Parbad.Gateway.ParbadVirtual;
using ProEShop.Common.Helpers;

var builder = WebApplication.CreateBuilder(args);
var parbadConnectionString = builder.Configuration.GetConnectionString("ParbadDataContextConnection");

// Add services to the container.
builder.Services.Configure<SiteSettings>(options => builder.Configuration.Bind(options));
builder.Services.Configure<ContentSecurityPolicyConfig>(options => builder.Configuration.GetSection("ContentSecurityPolicyConfig").Bind(options));
// Adds all of the ASP.NET Core Identity related services and configurations at once.
builder.Services.AddCustomIdentityServices();
builder.Services.AddParbad()
    .ConfigureStorage(builder =>
    {
        builder.UseEfCore(options =>
        {
            // Example 1: Using SQL Server
            var assemblyName = typeof(ApplicationDbContext).Assembly.GetName().Name;

            // Example 2: If you prefer to have a separate MigrationHistory table for Parbad, you can change the above line to this:
            options.ConfigureDbContext = db => db.UseSqlServer(parbadConnectionString, sql =>
            {
                sql.MigrationsAssembly(assemblyName);
            });

            options.DefaultSchema = "dbo"; // optional

            options.PaymentTableOptions.Name = "Payment"; // optional

            options.TransactionTableOptions.Name = "Transaction"; // optional
        });
    })
    .ConfigureGateways(gateways =>
    {
        gateways
        .AddZarinPal()
            .WithAccounts(source => source.Add<ParbadGatewaysAccounts>(ServiceLifetime.Transient));
        //.WithAccounts(accounts =>
        //{
        //    accounts.AddInMemory(account =>
        //    {
        //        account.MerchantId = "test";
        //        account.IsSandbox = true;
        //    });
        //});

        gateways
            .AddMellat()
            .WithAccounts(source => source.Add<ParbadGatewaysAccounts>(ServiceLifetime.Transient));
        //.WithAccounts(accounts =>
        //{
        //    accounts.AddInMemory(account =>
        //    {
        //        account.TerminalId = 123;
        //        account.UserName = "MyId";
        //        account.UserPassword = "MyPassword";
        //    });
        //});

        gateways
            .AddParbadVirtual()
            .WithOptions(options => options.GatewayPath = "/Cart/Payment/VirtualGateway");
    })
    .ConfigureHttpContext(builder => builder.UseDefaultAspNetCore());

builder.Services.AddRazorPages();
builder.Services.Configure<WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddMaps(Assembly.GetExecutingAssembly());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();
app.Services.InitializeDb();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseContentSecurityPolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.UseParbadVirtualGateway();

app.Run();
