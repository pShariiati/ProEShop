using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;

namespace ProEShop.Web.Pages.SellerPanel.Consignment;

public class CreateModel : SellerPanelBase
{
    #region Constructor

    private readonly IProductVariantService _productVariantService;

    public CreateModel(IProductVariantService productVariantService)
    {
        _productVariantService = productVariantService;
    }

    #endregion

    [BindProperty]
    [Display(Name = "˜Ï ÊäæÚ")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Range(1, int.MaxValue, ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public int VariantCode { get; set; }

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

        return Partial("_ProductVariantTrPartial", productVariant);
    }
}