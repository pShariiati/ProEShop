using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Entities.AuditableEntity;
using ProEShop.Services.Contracts;
using ProEShop.Web.Models;

namespace ProEShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _uow;
        private readonly ICategoryService _categoryService;

        public HomeController(
            ILogger<HomeController> logger,
            IUnitOfWork uow,
            ICategoryService categoryService,
            IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _uow = uow;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var cats = await _categoryService.GetAll();
            var firstCat = cats.First();
            var shadowProperty = _uow.GetShadowPropertyValue<DateTime>(firstCat, AuditableShadowProperties.CreatedDateTime);
            //var cats = await _categoryService.GetAll();
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
}