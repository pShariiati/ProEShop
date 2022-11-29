using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages;

public class CompareModel : PageModel
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public CompareModel(
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
}