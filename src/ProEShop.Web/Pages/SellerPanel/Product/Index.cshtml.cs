using AutoMapper;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.ProductVariants;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.SellerPanel.Product;

[CheckModelStateInRazorPages]
public class IndexModel : SellerPanelBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly ISellerService _sellerService;
    private readonly ICategoryService _categoryService;
    private readonly IUploadFileService _uploadFile;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IHtmlSanitizer _htmlSanitizer;
    private readonly IProductVariantService _productVariantService;

    public IndexModel(
        IProductService productService,
        ISellerService sellerService,
        ICategoryService categoryService,
        IUploadFileService uploadFile,
        IUnitOfWork uow,
        IHtmlSanitizer htmlSanitizer,
        IProductVariantService productVariantService,
        IMapper mapper)
    {
        _productService = productService;
        _sellerService = sellerService;
        _categoryService = categoryService;
        _uploadFile = uploadFile;
        _uow = uow;
        _htmlSanitizer = htmlSanitizer;
        _productVariantService = productVariantService;
        _mapper = mapper;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowProductsInSellerPanelViewModel Products { get; set; }
        = new();

    public void OnGet()
    {
        Products.SearchProducts.Categories = _categoryService.GetSellerCategories()
            .Result.CreateSelectListItem(firstItemText: "همه", firstItemValue: string.Empty);
    }

    public async Task<IActionResult> OnGetGetDataTableAsync()
    {
        return Partial("List", await _productService.GetProductsInSellerPanel(Products));
    }

    public async Task<IActionResult> OnGetGetProductDetails(long productId)
    {
        var product = await _productService.GetProductDetails(productId);
        if (product is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }
        return Partial("ProductDetails", product);
    }

    public async Task<IActionResult> OnGetAutocompleteSearchForPersianTitle(string term)
    {
        return Json(await _productService.GetPersianTitlesForAutocompleteInSellerPanel(term));
    }

    public async Task<IActionResult> OnGetShowProductVariantsAsync(long productId)
    {
        if (productId < 1)
        {
            return Json(new JsonResultOperation(false));
        }
        return Partial("ProductVariants", await _productVariantService.GetProductVariants(productId));
    }

    public async Task<IActionResult> OnGetEditProductVariant(long productVariantId)
    {
        if (productVariantId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var productVariant = await _productVariantService.GetDataForEdit(productVariantId);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        return Partial("_EditProductVariantPartial", productVariant);
    }

    public async Task<IActionResult> OnPostEditProductVariant(EditProductVariantViewModel model)
    {
        var productVariant = await _productVariantService.GetForEdit(model.Id);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        //_mapper.Map(model, productVariant);
        productVariant.Price = model.Price;
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "تنوع محصول مورد نظر با موفقیت ویرایش شد"));
    }

    public async Task<IActionResult> OnGetAddEditDiscount(long productVariantId)
    {
        if (productVariantId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var productVariant = await _productVariantService.GetDataForAddEditDiscount(productVariantId);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        return Partial("_AddEditDiscountPartial", productVariant);
    }

    public async Task<IActionResult> OnPostAddEditDiscount(AddEditDiscountViewModel model)
    {
        var productVariant = await _productVariantService.GetForEdit(model.Id);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        _mapper.Map(model, productVariant);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "تنوع محصول مورد نظر با موفقیت ویرایش شد"));
    }
}