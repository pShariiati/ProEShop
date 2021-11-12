using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEShop.Web.Areas.Identity.Controllers;

[Area("Identity")]
public class RegisterLoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
