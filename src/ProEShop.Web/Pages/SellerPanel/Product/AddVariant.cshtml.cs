using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Variants;

namespace ProEShop.Web.Pages.SellerPanel.Product;

public class AddVariantModel : SellerPanelBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly IGuaranteeService _guaranteeService;
    private readonly IMapper _mapper;
    private readonly ISellerService _sellerService;
    private readonly IProductVariantService _productVariantService;
    private readonly IUnitOfWork _uow;
    private readonly IVariantService _variantService;

    public AddVariantModel(
        IProductService productService,
        IMapper mapper,
        ISellerService sellerService,
        IProductVariantService productVariantService,
        IUnitOfWork uow,
        IGuaranteeService guaranteeService,
        IVariantService variantService)
    {
        _productService = productService;
        _mapper = mapper;
        _sellerService = sellerService;
        _productVariantService = productVariantService;
        _uow = uow;
        _guaranteeService = guaranteeService;
        _variantService = variantService;
    }

    #endregion

    [BindProperty]
    public AddVariantViewModel Variant { get; set; }

    public async Task<IActionResult> OnGet(long productId)
    {
        var productInfo = await _productService.GetProductInfoForAddVariant(productId);
        if (productInfo is null)
        {
            return RedirectToPage(PublicConstantStrings.Error404PageName);
        }

        Variant = productInfo;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        var checkInputs =
            await _variantService.CheckProductAndVariantTypeForForAddVariant(Variant.ProductId, Variant.VariantId);

        if (!checkInputs.IsSuccessful)
        {
            return Json(new JsonResultOperation(false));
        }

        var productVariantToAdd = _mapper.Map<Entities.ProductVariant>(Variant);
        if (checkInputs.IsVariantNull)
        {
            productVariantToAdd.VariantId = null;
        }
        productVariantToAdd.VariantCode = await _productVariantService.GetVariantCodeForCreateProductVariant();

        // Get seller id for entity
        var userId = User.Identity.GetLoggedInUserId();
        var sellerId = await _sellerService.GetSellerId(userId);
        productVariantToAdd.SellerId = sellerId;

        await _productVariantService.AddAsync(productVariantToAdd);

        // وضعیت محصول اگر جدید باشه
        // یعنی به تازگی ایجاد شده باشه و تنوعی نداشته باشه
        // اگر تنوعی براش اضافه بشه باید وضعیت محصول رو در حالت ناموجود قرار بدیم
        var product = await _productService.FindByIdWithIncludesAsync(Variant.ProductId, nameof(Entities.Product.Category));
        if (product.ProductStockStatus == ProductStockStatus.New)
        {
            product.ProductStockStatus = ProductStockStatus.Unavailable;
        }

        product.Category.HasVariant = true;

        await _uow.SaveChangesAsync();

        return Json(new JsonResultOperation(true, "تنوع محصول با موفقیت اضافه شد")
        {
            Data = Url.Page("SuccessfulProductVariant")
        });
    }

    public async Task<IActionResult> OnGetGetGuarantees(string input)
    {
        var result = await _guaranteeService
            .SearchOnGuaranteesForSelect2(input);

        var specificGuarantee = result.Select((value, index) => new { value, index })
            .SingleOrDefault(p => p.value.Text.Contains("0 ماهه"));

        if (specificGuarantee != null)
        {
            result[specificGuarantee.index] = new ShowSelect2DataByAjaxViewModel
            {
                Text = "گارانتی اصالت و سلامت فیزیکی کالا",
                Id = specificGuarantee.value.Id
            };
        }

        return Json(new
        {
            results = result
        });
    }
}