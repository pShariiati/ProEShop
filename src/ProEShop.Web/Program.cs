using DNTCommon.Web.Core;
using ProEShop.IocConfig;
using ProEShop.ViewModels.Identity.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<SiteSettings>(options => builder.Configuration.Bind(options));
builder.Services.Configure<ContentSecurityPolicyConfig>(options => builder.Configuration.GetSection("ContentSecurityPolicyConfig").Bind(options));
// Adds all of the ASP.NET Core Identity related services and configurations at once.
builder.Services.AddCustomIdentityServices();
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapDefaultControllerRoute();

app.Run();
