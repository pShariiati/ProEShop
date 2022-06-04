using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.SellerPanel.Product;

public class AllModel : SellerPanelBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly ISellerService _sellerService;
    private readonly ICategoryService _categoryService;
    private readonly IUploadFileService _uploadFile;
    private readonly IUnitOfWork _uow;
    private readonly IHtmlSanitizer _htmlSanitizer;

    public AllModel(
        IProductService productService,
        ISellerService sellerService,
        ICategoryService categoryService,
        IUploadFileService uploadFile,
        IUnitOfWork uow,
        IHtmlSanitizer htmlSanitizer)
    {
        _productService = productService;
        _sellerService = sellerService;
        _categoryService = categoryService;
        _uploadFile = uploadFile;
        _uow = uow;
        _htmlSanitizer = htmlSanitizer;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowAllProductsInSellerPanelViewModel Products { get; set; }
        = new();

    public void OnGet()
    {
        Products.SearchProducts.Categories = _categoryService.GetCategoriesWithNoChild()
            .Result.CreateSelectListItem(firstItemText: "еге", firstItemValue: string.Empty);
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
        return Partial("ListForAll", await _productService.GetAllProductsInSellerPanel(Products));
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
        return Json(await _productService.GetPersianTitlesForAutocomplete(term));
    }
}