using AutoMapper;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Admin.Product;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly ISellerService _sellerService;
    private readonly ICategoryService _categoryService;

    public IndexModel(
        IProductService productService,
        ISellerService sellerService,
        ICategoryService categoryService)
    {
        _productService = productService;
        _sellerService = sellerService;
        _categoryService = categoryService;
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

    public async Task<IActionResult> OnGetAutocompleteSearchForPersianTitle(string term)
    {
        return Json(await _productService.GetPersianTitlesForAutocomplete(term));
    }

    public async Task<IActionResult> OnGetAutocompleteSearchForShopName(string term)
    {
        return Json(await _sellerService.GetShopNamesForAutocomplete(term));
    }
}