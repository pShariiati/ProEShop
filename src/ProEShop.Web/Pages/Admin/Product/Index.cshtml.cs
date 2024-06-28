using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.Admin.Product;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly ISellerService _sellerService;
    private readonly ICategoryService _categoryService;
    private readonly IProductShortLinkService _productShortLinkService;
    private readonly IUploadFileService _uploadFile;
    private readonly IUnitOfWork _uow;
    private readonly IHtmlSanitizer _htmlSanitizer;

    public IndexModel(
        IProductService productService,
        ISellerService sellerService,
        ICategoryService categoryService,
        IUploadFileService uploadFile,
        IUnitOfWork uow,
        IHtmlSanitizer htmlSanitizer,
        IProductShortLinkService productShortLinkService)
    {
        _productService = productService;
        _sellerService = sellerService;
        _categoryService = categoryService;
        _uploadFile = uploadFile;
        _uow = uow;
        _htmlSanitizer = htmlSanitizer;
        _productShortLinkService = productShortLinkService;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowProductsViewModel Products { get; set; }
        = new();

    public void OnGet()
    {
        Products.SearchProducts.Categories = _categoryService.GetCategoriesWithNoChild()
            .Result.CreateSelectListItem(firstItemText: "همه", firstItemValue: string.Empty);
    }

    public async Task<IActionResult> OnGetGetDataTableAsync()
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _productService.GetProducts(Products));
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

    public async Task<IActionResult> OnPostRemoveProduct(long id)
    {
        if (id < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var product = await _productService.GetProductToRemoveInManagingProducts(id);
        if (product is null)
        {
            return Json(new JsonResultOperation(false, "محصول مورد نظر یافت نشد"));
        }

        var productShortLink = await _productShortLinkService.FindByIdAsync(product.ProductShortLinkId);
        productShortLink.IsUsed = false;

        _productService.Remove(product);
        await _uow.SaveChangesAsync();
        foreach (var media in product.ProductMedia)
        {
            _uploadFile.DeleteFile(media.FileName,
                media.IsVideo ? "videos" : "images", "products");
        }
        return Json(new JsonResultOperation(true, "محصول مورد نظر با موفقیت حذف شد"));
    }

    public async Task<IActionResult> OnPostRejectProduct(ProductDetailsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, "لطفا دلایل رد کردن محصول را وارد نمایید"));
        }

        var product = await _productService.FindByIdAsync(model.Id);
        if (product is null)
        {
            return Json(new JsonResultOperation(false, "محصول مورد نظر یافت نشد"));
        }

        product.Status = ProductStatus.Rejected;
        product.RejectReason = _htmlSanitizer.Sanitize(model.RejectReason);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "محصول مورد نظر با موفقیت رد شد"));
    }

    public async Task<IActionResult> OnPostConfirmProduct(long id, Dimension dimension)
    {
        if (id < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var product = await _productService.FindByIdAsync(id);
        if (product is null)
        {
            return Json(new JsonResultOperation(false, "محصول مورد نظر یافت نشد"));
        }

        product.Status = ProductStatus.Confirmed;
        product.Dimension = dimension;
        product.RejectReason = null;
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "محصول مورد نظر با موفقیت تایید شد"));
    }

    public async Task<IActionResult> OnGetAutocompleteSearchForPersianTitle(string term)
    {
        return Json(await _productService.GetPersianTitlesForAutocomplete(term));
    }

    public async Task<IActionResult> OnGetAutocompleteSearchForShopName(string term)
    {
        return Json(await _sellerService.GetShopNamesForAutocomplete(term));
    }
}