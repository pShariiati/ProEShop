using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Helpers;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.Compare;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IViewRendererService _viewRendererService;

    public IndexModel(
        IProductService productService,
        ICategoryService categoryService,
        IViewRendererService viewRendererService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _viewRendererService = viewRendererService;
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

    /// <summary>
    /// گرفتن محصولات برای مودال افزودن محصول در صفحه مقایسه
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetShowAddProduct(int pageNumber = 1)
    {
        var result = await _productService.GetProductsForAddProductInCompare(pageNumber);

        // چرا برای این ایف از پارامتر ورودی هندلر استفاده نکردیم ؟
        // چون امکان داره که عدد صفر رو وارد کنه
        // و اگه صفر وارد بشه، وارد الس میشه و "پارشل صفحه یک به بالا" رو نمایش میده
        // به خاطر همین از پیج نامبری استفاده میکنیم که مطمئنیم عدد درست رو داره
        // یعنی پیج نامبری که خودمون داخل سرویس مقدار دهی میکنیم
        if (result.PageNumber == 1)
        {
            // صفحه یک
            // کل محتویات داخل مودال رو تغییر میدیم
            return Json(new JsonResultOperation(true, string.Empty)
            {
                // به تعداد محصولات نیاز نداریم
                // چون در داخل خود پارشل، تعداد محصولات نمایش داده میشه
                Data = new
                {
                    ProductsBody = await _viewRendererService
                        .RenderViewToStringAsync("~/Pages/Compare/_AddProductPartial.cshtml",
                            result),
                    PageNumber = result.PageNumber,
                    IsLastPage = result.IsLastPage
                }
            });
        }
        else
        {
            // صفحه یک به بالا
            // تنها بخش محصولات رو تغییر میدیم
            return Json(new JsonResultOperation(true, string.Empty)
            {
                Data = new
                {
                    ProductsBody = await _viewRendererService
                        .RenderViewToStringAsync("~/Pages/Compare/_ProductsInAddProductPartial.cshtml",
                            result.Products),
                    ProductsCount = result.Count,
                    PageNumber = result.PageNumber,
                    IsLastPage = result.IsLastPage
                }
            });
        }
    }
}