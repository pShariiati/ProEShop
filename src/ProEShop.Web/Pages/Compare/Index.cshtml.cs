using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.Compare;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public IndexModel(
        IProductService productService,
        ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    #endregion

    public List<ShowProductInCompareViewModel> Products { get; set; }

    public async Task<IActionResult> OnGet(int productCode1, int productCode2, int productCode3, int productCode4)
    {
        if (productCode1 < 1)
        {
            return NotFound();
        }

        if (!await _categoryService
                .CheckProductCategoryIdsInComparePage(productCode1, productCode2, productCode3, productCode4))
        {
            return BadRequest();
        }

        Products = await _productService
            .GetProductsForCompare(productCode1, productCode2, productCode3, productCode4);

        return Page();
    }

    public async Task<IActionResult> OnGetGetProductsForCompare(int productCode1, int productCode2, int productCode3, int productCode4)
    {
        if (productCode1 < 1)
        {
            return RecordNotFound();
        }

        if (!await _categoryService
                .CheckProductCategoryIdsInComparePage(productCode1, productCode2, productCode3, productCode4))
        {
            return JsonBadRequest();
        }

        Products = await _productService
            .GetProductsForCompare(productCode1, productCode2, productCode3, productCode4);

        return Partial("_CompareBodyPartial", Products);
    }

    public async Task<IActionResult> OnGetShowAddProduct()
    {
        var products = await _productService.GetProductsForAddProductInCompare();
        return Partial("_AddProductPartial", products);
    }
}