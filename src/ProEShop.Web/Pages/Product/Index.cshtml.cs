using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.FlowAnalysis;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.ProductComments;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.QuestionsAndAnswers;

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
    private readonly ICommentScoreService _commentScoreService;
    private readonly IAnswerScoreService _answerScoreService;
    private readonly IQuestionAndAnswerService _questionAndAnswerService;

    public IndexModel(
        IProductService productService,
        IUserProductFavoriteService userProductFavoriteService,
        IUnitOfWork uow,
        IProductVariantService productVariantService,
        ICartService cartService,
        IViewRendererService viewRendererService,
        ICommentReportService commentReportService,
        IProductCommentService productCommentService,
        ICommentScoreService commentScoreService,
        IAnswerScoreService answerScoreService,
        IQuestionAndAnswerService questionAndAnswerService)
    {
        _productService = productService;
        _userProductFavoriteService = userProductFavoriteService;
        _uow = uow;
        _productVariantService = productVariantService;
        _cartService = cartService;
        _viewRendererService = viewRendererService;
        _commentReportService = commentReportService;
        _productCommentService = productCommentService;
        _commentScoreService = commentScoreService;
        _answerScoreService = answerScoreService;
        _questionAndAnswerService = questionAndAnswerService;
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
        var userId = User.Identity.GetLoggedInUserId();

        // آیدی کامنت هایی که در صفحه نمایش داده میشوند
        var commentIds = ProductInfo.ProductComments
            .Select(x => x.Id)
            .ToArray();

        // آیدی سوالاتی که در صفحه نمایش داده میشوند
        var questionIds = ProductInfo.ProductsQuestionsAndAnswers
            .SelectMany(x => x.Answers)
            .Select(x => x.Id)
            .ToArray();

        // از داخل کامنت هایی که در صفحه نمایش داده میشوند کدام یک توسط این کاربر
        // لایک و یا دیسلایک روی آنها انجام شده است
        ProductInfo.LikedCommentsByUser = await _commentScoreService.GetLikedCommentsLikedByUser(userId, commentIds);

        // از داخل جواب هایی که در صفحه نمایش داده میشوند کدام یک توسط این کاربر
        // لایک و یا دیسلایک روی آنها انجام شده است
        ProductInfo.LikedAnswersByUser = await _answerScoreService.GetLikedAnswersLikedByUser(userId, questionIds);

        // نظرات این محصول در چند صفحه نمایش داده میشوند
        ProductInfo.CommentsPagesCount = (int)Math.Ceiling(
            (decimal)ProductInfo.ProductCommentsCount / 2
        );

        // سوالات این محصول در چند صفحه نمایش داده میشوند
        ProductInfo.QuestionsPagesCount = (int)Math.Ceiling(
            (decimal)ProductInfo.ProductQuestionsCount / 2
        );

        // آیدی های تنوع های این محصول
        
        var productVariantsIds = ProductInfo.ProductVariants.Select(x => x.Id).ToList();
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

        var model = new CommentForCommentPartialViewModel()
        {
            CurrentPage = pageNumber,
            CommentsPagesCount = commentsPagesCount,
            ProductComments = comments
        };

        var userId = User.Identity.GetUserId();

        if (userId != null)
        {
            var commentIds = comments
                .Select(x => x.Id)
                .ToArray();

            model.LikedCommentsByUser = await _commentScoreService
                .GetLikedCommentsLikedByUser(userId.Value, commentIds);
        }

        return Partial("_CommentsPartial", model);
    }

    /// <summary>
    /// لایک و دیسلایک کامنت ها
    /// </summary>
    /// <param name="commentId"></param>
    /// <param name="isLike"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnPostCommentScore(long commentId, bool isLike)
    {
        var userId = User.Identity.GetUserId();

        if (userId is null)
        {
            return JsonBadRequest();
        }

        if (!await _productCommentService.IsExistsBy(nameof(Entities.ProductComment.Id), commentId))
        {
            return JsonBadRequest();
        }

        var commentScore = await _commentScoreService.FindAsync(userId.Value, commentId);

        var operation = string.Empty;

        // اگر وجود نداشته باشه اضافه میکنیم
        if (commentScore is null)
        {
            operation = "Add";

            await _commentScoreService.AddAsync(new CommentScore()
            {
                IsLike = isLike,
                ProductCommentId = commentId,
                UserId = userId.Value
            });
        }
        // اگر کاربر لایک کرده بود و بعد دوباره روی دکمه لایک کلیک کرد باید لایک کاربر رو حذف کنیم
        // اگر کاربر دیسلایک کرده بود و بعد دوباره روی دکمه دیسلایک کلیک کرد باید دیسلایک کاربر رو حذف کنیم
        else if (commentScore.IsLike && isLike || !commentScore.IsLike && !isLike)
        {
            operation = "Subtract";

            _commentScoreService.Remove(commentScore);
        }
        // اگر کاربر لایک کرده بود و بعد روی دیسلایک کلیک کرد باید ایز لایک رو به فالس تغییر بدیم
        // اگر کاربر دیسلایک کرده بود و بعد روی لایک کلیک کرد باید ایز لایک رو به ترو تغییر بدیم
        else
        {
            operation = "AddAndSubtract";
            commentScore.IsLike = !commentScore.IsLike;
        }

        await _uow.SaveChangesAsync();

        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = operation
        });
    }

    /// <summary>
    /// گرفتن سوالات محصولات به صورت صفحه بندی
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="pageNumber"></param>
    /// <param name="questionsPagesCount">برای اینکه تعداد صفحات سوالات رو سمت سرور مجددامحاسبه نکنیم
    /// از همون مقدار سمت کلاینت که قبلا محاسبه شده استفاده میکنیم</param>
    /// /// <param name="sortBy"></param>
    /// /// <param name="orderBy"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetShowQuestionsByPagination(long productId, int pageNumber, int questionsPagesCount, QuestionsSortingForProductInfo sortBy, SortingOrder orderBy)
    {
        if (!await _productService.IsExistsBy(nameof(Entities.Product.Id), productId))
        {
            return JsonBadRequest();
        }

        var questions = await _questionAndAnswerService.GetQuestionsByPagination(productId, pageNumber, sortBy, orderBy);

        var model = new QuestionAndAnswerForQuestionAndAnswerPartialViewModel()
        {
            CurrentPage = pageNumber,
            QuestionsAndAnswersPagesCount = questionsPagesCount,
            ProductQuestionsAndAnswers = questions
        };

        var userId = User.Identity.GetUserId();

        if (userId != null)
        {
            var answerIds = questions
                .SelectMany(x => x.Answers)
                .Select(x => x.Id)
                .ToArray();

            model.LikedAnswersByUser = await _answerScoreService
                .GetLikedAnswersLikedByUser(userId.Value, answerIds);
        }

        return Partial("_QuestionsAndAnswersPartial", model);
    }

    /// <summary>
    /// لایک و دیسلایک جواب های سوالات
    /// </summary>
    /// <param name="answerId"></param>
    /// <param name="isLike"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnPostQuestionScore(long answerId, bool isLike)
    {
        var userId = User.Identity.GetUserId();

        if (userId is null)
        {
            return JsonBadRequest();
        }

        if (!await _questionAndAnswerService.IsExistsAndAnswer(answerId))
        {
            return JsonBadRequest();
        }

        var answerScore = await _answerScoreService.FindAsync(userId.Value, answerId);

        var operation = string.Empty;

        // اگر وجود نداشته باشه اضافه میکنیم
        if (answerScore is null)
        {
            operation = "Add";

            await _answerScoreService.AddAsync(new ProductQuestionAnswerScore()
            {
                IsLike = isLike,
                AnswerId = answerId,
                UserId = userId.Value
            });
        }
        // اگر کاربر لایک کرده بود و بعد دوباره روی دکمه لایک کلیک کرد باید لایک کاربر رو حذف کنیم
        // اگر کاربر دیسلایک کرده بود و بعد دوباره روی دکمه دیسلایک کلیک کرد باید دیسلایک کاربر رو حذف کنیم
        else if (answerScore.IsLike && isLike || !answerScore.IsLike && !isLike)
        {
            operation = "Subtract";

            _answerScoreService.Remove(answerScore);
        }
        // اگر کاربر لایک کرده بود و بعد روی دیسلایک کلیک کرد باید ایز لایک رو به فالس تغییر بدیم
        // اگر کاربر دیسلایک کرده بود و بعد روی لایک کلیک کرد باید ایز لایک رو به ترو تغییر بدیم
        else
        {
            operation = "AddAndSubtract";
            answerScore.IsLike = !answerScore.IsLike;
        }

        await _uow.SaveChangesAsync();

        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = operation
        });
    }
}