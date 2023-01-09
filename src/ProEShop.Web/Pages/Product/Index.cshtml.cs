using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.ProductComments;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.Product;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly IUserProductFavoriteService _userProductFavoriteService;
    private readonly IUnitOfWork _uow;
    private readonly IProductVariantService _productVariantService;
    private readonly ICartService _cartService;
    private readonly IViewRendererService _viewRendererService;
    private readonly ICommentReportService _commentReportService;
    private readonly IProductCommentService _productCommentService;

    public IndexModel(
        IProductService productService,
        IUserProductFavoriteService userProductFavoriteService,
        IUnitOfWork uow,
        IProductVariantService productVariantService,
        ICartService cartService,
        IViewRendererService viewRendererService,
        ICommentReportService commentReportService,
        IProductCommentService productCommentService)
    {
        _productService = productService;
        _userProductFavoriteService = userProductFavoriteService;
        _uow = uow;
        _productVariantService = productVariantService;
        _cartService = cartService;
        _viewRendererService = viewRendererService;
        _commentReportService = commentReportService;
        _productCommentService = productCommentService;
    }

    #endregion

    public ShowProductInfoViewModel ProductInfo { get; set; }

    public async Task<IActionResult> OnGet(int productCode, string slug)
    {
        ProductInfo = await _productService.GetProductInfo(productCode);
        if (ProductInfo is null)
        {
            return RedirectToPage(PublicConstantStrings.Error404PageName);
        }

        if (ProductInfo.Slug != slug)
        {
            return RedirectToPage("Index", new
            {
                productCode = productCode,
                slug = ProductInfo.Slug
            });
        }

        // نظرات این محصول در چند صفحه نمایش داده میشوند
        ProductInfo.CommentsPagesCount = (int)Math.Ceiling(
            (decimal)ProductInfo.ProductCommentsCount / 2
        );

        // آیدی های تنوع های این محصول
        var productVariantsIds = ProductInfo.ProductVariants.Select(x => x.Id).ToList();
        var userId = User.Identity.GetLoggedInUserId();
        // تنوع های این محصول که در سبد خرید این کاربری که، صفحه رو لود میکنه قرار داره
        ProductInfo.ProductVariantsInCart = await _cartService.GetProductVariantsInCart(productVariantsIds, userId);
        return Page();
    }

    public async Task<IActionResult> OnPostAddOrRemoveFavorite(long productId, bool addFavorite)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Json(new JsonResultOperation(false));
        }

        if (!await _productService.IsExistsBy(nameof(Entities.Product.Id), productId))
        {
            return Json(new JsonResultOperation(false));
        }

        var userId = User.Identity.GetLoggedInUserId();

        var userProductFavorite = await _userProductFavoriteService.FindAsync(userId, productId);
        if (userProductFavorite is null && addFavorite)
        {
            await _userProductFavoriteService.AddAsync(new Entities.UserProductFavorite
            {
                ProductId = productId,
                UserId = userId
            });
        }
        else if (userProductFavorite != null && !addFavorite)
        {
            _userProductFavoriteService.Remove(userProductFavorite);
        }

        await _uow.SaveChangesAsync();

        return Json(new JsonResultOperation(true, string.Empty));
    }

    public async Task<IActionResult> OnPostAddProductVariantToCart(long productVariantId, bool isIncrease)
    {
        var productVariant = await _productVariantService.FindByIdAsync(productVariantId);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false));
        }

        var userId = User.Identity.GetLoggedInUserId();

        var cart = await _cartService.FindAsync(userId, productVariantId);
        if (cart is null)
        {
            var cartToAdd = new Entities.Cart()
            {
                ProductVariantId = productVariantId,
                UserId = userId,
                Count = 1
            };
            await _cartService.AddAsync(cartToAdd);
        }
        else if (isIncrease)
        {
            // فروشنده تعیین کرده که حداکثر تعدادی که کاربر طی هر خرید میتونه
            // از این محصول وارد سبد خرید کنه و خریدشو انجام بده 3 مورد است

            // مقدار داخل سبد خرید قبل فشردن دکمه به علاوه
            // 3
            cart.Count++;
            // بعد از زدن دکمه به علاوه
            // 4

            // چون تعداد داخل سبد خرید بیشتر از مقداری هست که فروشنده تعیین کرده
            // در نتیجه مقدار داخل سبد خرید رو به مقدار تعیین شده توسط فروشنده تغییر میدیم
            if (cart.Count > productVariant.MaxCountInCart)
                cart.Count = productVariant.MaxCountInCart;

            // موجودی انبار 2 عدد است
            // موجودی داخل سبد خرید هم 2 عدد است
            // حالا روی دکمه به علاوه کلیک میشه
            // چون حداکثر تعدادی که فروشنده تعیین کرده 3 است
            // در نتیجه از ایف بالا عبور میکنه و به ایف پایین میرسه
            // موقعی که روی دکمه به علاوه کلیک میشه
            // تعداد داخل سبد خرید میشه 3
            // و چون 3 بزرگتر از موجودی انبار یعنی 2 است
            // در نتیجه مقدار داخل سبد خرید هم به 2 تغییر میدیم

            // productVariant.Count => موجودی انبار برای این تنوع
            if (cart.Count > productVariant.Count)
                cart.Count = (short)productVariant.Count;
        }
        else
        {
            cart.Count--;
            if (cart.Count == 0)
            {
                _cartService.Remove(cart);
            }
        }

        await _uow.SaveChangesAsync();

        // اگر کاونت سبد خرید برابر با مکس تعیین شده توسط فروشنده بود
        // یا مساوی تعداد موجودی داخل انبار، این متغیر ترو میشود
        var isCartFull = productVariant.MaxCountInCart == (cart?.Count ?? 1)
                         ||
                         (cart?.Count ?? 1) == productVariant.Count;

        var carts = await _cartService.GetCartsForDropDown(userId);

        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = new
            {
                Count = cart?.Count ?? 1,
                ProductVariantId = productVariantId,
                IsCartFull = isCartFull,
                CartsDetails = await _viewRendererService.RenderViewToStringAsync("~/Pages/Shared/_CartPartial.cshtml", carts)
            }
        });
    }

    /// <summary>
    /// افزودن گزارش دیدگاه
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnPostAddCommentReport(long commentId)
    {
        var userId = User.Identity.GetUserId();

        // اگر کاربر لاگین نباشه نال میشه
        if (userId is null)
        {
            return Json(new JsonResultOperation(false));
        }

        // آیا کامنت وجود داره ؟
        if (!await _productCommentService.IsExistsBy(nameof(Entities.ProductComment.Id), commentId))
        {
            return JsonBadRequest();
        }

        // آیا از قبل گزارش ثبت کرده یا خیر
        // اگر این بررسی را انجام ندهیم و یک مورد تکراری اضافه شود
        // اکسپشن ایجاد میشود
        if (await _commentReportService.IsExistsBy(
                nameof(Entities.CommentReport.UserId), nameof(Entities.CommentReport.ProductCommentId)
                , userId, commentId
            ))
        {
            return Json(new JsonResultOperation(false, "شما از قبل این دیدگاه را گزارش داده بودید"));
        }

        // افزودن گزارش کامنت
        await _commentReportService.AddAsync(new CommentReport()
        {
            UserId = userId.Value,
            ProductCommentId = commentId
        });

        await _uow.SaveChangesAsync();

        return Json(new JsonResultOperation(true, "گزارش این دیدگاه با موفقیت ثبت شد"));
    }

    /// <summary>
    /// گرفتن نظرات محصولات به صورت صفحه بندی
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="pageNumber"></param>
    /// <param name="commentsPagesCount">برای اینکه تعداد صفحات نظرات رو سمت سرور مجددامحاسبه نکنیم
    /// از همون مقدار سمت کلاینت که قبلا محاسبه شده استفاده میکنیم</param>
    /// /// <param name="sortBy"></param>
    /// /// <param name="orderBy"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetShowCommentsByPagination(long productId, int pageNumber, int commentsPagesCount, CommentsSortingForProductInfo sortBy, SortingOrder orderBy)
    {
        if (!await _productService.IsExistsBy(nameof(Entities.Product.Id), productId))
        {
            return JsonBadRequest();
        }

        var comments = await _productCommentService.GetCommentsByPagination(productId, pageNumber, sortBy, orderBy);

        return Partial("_CommentsPartial", (comments, commentsPagesCount, pageNumber));
    }
}