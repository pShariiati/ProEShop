using AutoMapper;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.ProductShortLinks;

namespace ProEShop.Web.Pages.Admin.ProductShortLink;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IProductShortLinkService _productShortLinkService;
    private readonly IUnitOfWork _uow;

    public IndexModel(
        IProductShortLinkService productShortLinkService,
        IUnitOfWork uow)
    {
        _productShortLinkService = productShortLinkService;
        _uow = uow;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowProductShortLinksViewModel ProductShortLinks { get; set; }
        = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetGetDataTableAsync()
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, PublicConstantStrings.ModelStateErrorMessage);
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _productShortLinkService.GetProductShortLinks(ProductShortLinks));
    }

    public async Task<IActionResult> OnPostDelete(long shortLinkId)
    {
        if (shortLinkId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var shortLink = await _productShortLinkService.GetForDelete(shortLinkId);
        if (shortLink is null)
        {
            return Json(new JsonResultOperation(false));
        }

        _productShortLinkService.Remove(shortLink);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "لینک مورد نظر با موفقیت حذف شد"));
    }
}