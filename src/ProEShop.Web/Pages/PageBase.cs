using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProEShop.Web.Pages;

public class PageBase : PageModel
{
    public JsonResult Json(object input)
    {
        return new(input);
    }
}