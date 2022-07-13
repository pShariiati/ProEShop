using Microsoft.AspNetCore.Mvc;

namespace ProEShop.Web.ViewComponents;

public class MainMenuViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}