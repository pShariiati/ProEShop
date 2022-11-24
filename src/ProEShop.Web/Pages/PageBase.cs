using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;

namespace ProEShop.Web.Pages;

public class PageBase : PageModel
{
    public JsonResult Json(object input)
    {
        return new(input);
    }

    public JsonResult RecordNotFound(string message = null)
    {
        return Json(new JsonResultOperation(false, message ?? PublicConstantStrings.RecordNotFoundMessage));
    }
}