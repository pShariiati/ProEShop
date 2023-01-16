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

    public JsonResult JsonBadRequest(string message = null)
    {
        return Json(new JsonResultOperation(false, message ?? "خطایی به وجود آمد"));
    }

    public JsonResult JsonOk(string message, object data = null)
    {
        return Json(new JsonResultOperation(true, message)
        {
            Data = data
        });
    }
}