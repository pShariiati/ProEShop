using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProEShop.ViewModels.Identity.Settings;
using ProEShop.Web.Models;

namespace ProEShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOptionsSnapshot<SiteSettings> siteSettings;

    public HomeController(ILogger<HomeController> logger, IOptionsSnapshot<SiteSettings> siteSettings)
    {
        _logger = logger;
        this.siteSettings = siteSettings;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
