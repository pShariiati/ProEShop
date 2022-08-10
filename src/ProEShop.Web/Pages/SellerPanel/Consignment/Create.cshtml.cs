using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Web.Pages.SellerPanel.Consignment;

public class CreateModel : SellerPanelBase
{
    #region Constructor

    private readonly IProductVariantService _productVariantService;
    private readonly IViewRendererService _viewRendererService;
    private readonly IConsignmentService _consignmentService;
    private readonly IUnitOfWork _uow;
    private readonly ISellerService _sellerService;

    public CreateModel(
        IProductVariantService productVariantService,
        IViewRendererService viewRendererService,
        IConsignmentService consignmentService,
        IUnitOfWork uow,
        ISellerService sellerService)
    {
        _productVariantService = productVariantService;
        _viewRendererService = viewRendererService;
        _consignmentService = consignmentService;
        _uow = uow;
        _sellerService = sellerService;
    }

    #endregion
    
    [Display(Name = "کد تنوع")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Range(1, int.MaxValue, ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public int VariantCode { get; set; }

    public CreateConsignmentViewModel CreateConsignment { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(CreateConsignmentViewModel createConsignment)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        if (createConsignment.Variants.Count < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var deliveryDate = createConsignment.DeliveryDate.ToGregorianDate();
        if (!deliveryDate.IsSuccessful)
        {
            return Json(new JsonResultOperation(false));
        }

        // 1|||50
        // 2|||30
        var variantCodes = new List<int>();
        foreach (var variantCode in createConsignment.Variants)
        {
            var splitVariant = variantCode.Split("|||");
            if (!int.TryParse(splitVariant[0], out var variantCodeToAdd))
            {
                return Json(new JsonResultOperation(false));
            }
            variantCodes.Add(variantCodeToAdd);
        }
        // رکورد تکراری نباید داخل کد های تنوع وجود داشته باشد
        // 10
        // 8
        if (createConsignment.Variants.Count != createConsignment.Variants.Distinct().Count())
        {
            return Json(new JsonResultOperation(false));
        }

        var consignmentToAdd = new Entities.Consignment
        {
            DeliveryDate = deliveryDate.Result,
            SellerId = await _sellerService.GetSellerId()
        };

        // تنوع های اومده از طرف فروشنده رو از پایگاه داده میخونیم
        // آیدی برای استفاده در جدول آیتم های محموله
        // کد تنوع هم برای جستجو در داخل ورودی های کاربر که بفهمیم
        // Count
        // وارد شده هر محصول چه تعداد است
        var productVariants = await _productVariantService
            .GetProductVariantsForCreateConsignment(variantCodes);

        // به همان تعداد که تنوع، از طرف کاربر وارد شده است
        // باید به همان تعداد، تنوع را از پایگاه داده بخوانیم
        if (productVariants.Count != variantCodes.Count)
        {
            return Json(new JsonResultOperation(false));
        }

        foreach (var productVariant in productVariants)
        {
            // 1|||20
            var variantCodeToCompare = $"{productVariant.VariantCode}|||";
            var variantItem = createConsignment.Variants
                .Single(x => x.StartsWith(variantCodeToCompare));
            var productCountString = variantItem.Split("|||")[1];
            if (!int.TryParse(productCountString, out var productCount))
            {
                return Json(new JsonResultOperation(false));
            }

            var maxProductCount = 100000;
            if (productCount > maxProductCount)
            {
                return Json(new JsonResultOperation(false,
                    $"تعداد هر محصول باید بین 1 تا {maxProductCount}"));
            }
            consignmentToAdd.ConsignmentItems.Add(new ConsignmentItem()
            {
                Count = productCount,
                ProductVariantId = productVariant.Id,
                Barcode = $"{productVariant.Id}--{consignmentToAdd.SellerId}"
            });
        }

        
        await _consignmentService.AddAsync(consignmentToAdd);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "محموله مورد نظر ایجاد شد")
        {
            Data = Url.Page("ConfirmationConsignment")
        });
    }

    public async Task<IActionResult> OnPostGetConsignmentTr(int variantCode)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        var productVariant = await _productVariantService.GetProductVariantForCreateConsignment(variantCode);
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