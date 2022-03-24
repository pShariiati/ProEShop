using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Helpers;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.SellerPanel.Product;

public class CreateModel : SellerPanelBase
{
    #region Constructor

    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;

    public CreateModel(
        ICategoryService categoryService,
        IBrandService brandService)
    {
        _categoryService = categoryService;
        _brandService = brandService;
    }

    #endregion

    public AddProductViewModel Product { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetGetCategories(long[] selectedCategoriesIds)
    {
        var result = await _categoryService.GetCategoriesForCreateProduct(selectedCategoriesIds);
        return Partial("_SelectProductCategoryPartial", result);
    }

    public async Task<IActionResult> OnGetGetCategoryBrands(long categoryId)
    {
        if (categoryId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var brands = await _brandService.GetBrandsByCategoryId(categoryId);
        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = brands
        });
    }

    public async Task<IActionResult> OnGetCanAddFakeProduct(long categoryId)
    {
        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = await _categoryService.CanAddFakeProduct(categoryId)
        });
    }
}