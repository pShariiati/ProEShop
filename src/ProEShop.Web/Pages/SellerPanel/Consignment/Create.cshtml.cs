using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Web.Pages.SellerPanel.Consignment;

public class CreateModel : SellerPanelBase
{
    #region Constructor

    private readonly IProductVariantService _productVariantService;
    private readonly IViewRendererService _viewRendererService;

    public CreateModel(
        IProductVariantService productVariantService,
        IViewRendererService viewRendererService)
    {
        _productVariantService = productVariantService;
        _viewRendererService = viewRendererService;
    }

    #endregion

    [BindProperty]
    [Display(Name = "˜Ï ÊäæÚ")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Range(1, int.MaxValue, ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public int VariantCode { get; set; }

    public CreateConsignmentViewModel CreateConsignment { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostGetConsignmentTr()
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        var productVariant = await _productVariantService.GetProductVariantForCreateConsignment(VariantCode);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = await _viewRendererService.RenderViewToStringAsync(
                "~/Pages/SellerPanel/Consignment/_ProductVariantTrPartial.cshtml",
                productVariant)
        });
    }
}