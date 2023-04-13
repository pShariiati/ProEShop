using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Search;

namespace ProEShop.Web.Pages.Search;

public class ShowCategoryModel : PageModel
{
    #region Constructor

    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public ShowCategoryModel(
        ICategoryService categoryService,
        IProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    #endregion

    public SearchOnCategoryViewModel SearchOnCategory { get; set; }

    public async Task OnGet(string categorySlug, string brandSlug)
    {
        SearchOnCategory = await _categoryService.GetSearchOnCategoryData(categorySlug, brandSlug);
    }

    /// <summary>
    /// نمایش محصولات به صورت صفحه بندی شده
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetShowProductsByPagination(SearchOnCategoryInputsViewModel inputs)
    {
        var result = await _productService.GetProductsByPaginationForSearch(inputs);
        return Partial("_Products", result);
    }
}