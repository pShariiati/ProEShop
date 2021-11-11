using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProEShop.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}