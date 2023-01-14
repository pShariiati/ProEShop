using ProEShop.ViewModels.Products;

namespace ProEShop.ViewModels.ProductComments;

/// <summary>
/// ویوو مدل پارشلِ کامنت های صفحه تکی محصول
/// </summary>
public class CommentForCommentPartialViewModel
{
    /// <summary>
    /// صفحه جاری
    /// داخل چه صفحه ایی هستیم ؟
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// تعداد کل صفحات
    /// </summary>
    public int CommentsPagesCount { get; set; }

    /// <summary>
    /// از داخل کل کامنت هایی که نمایش داده میشود
    /// کدام یک توسط این کاربر لایک و یا دیسلایک شده است ؟
    /// اگر کاربر وارد سیستم نباشه این مورد نال میشه و دیگه نمیتونیم داخل
    /// پارشل از سینگل اور دیفالت استفاده کنیم پس یک مقدار پیش فرض رو براش
    /// در نظر میگیریم که اگر نال بود هنگامی که از سینگل اور دیفالت استفاده میکنیم
    /// دچار خطا نشویم، چون نمیوان روی یک شیء نال از سینگل اور دیفالت استفاده کرد
    /// </summary>
    public List<LikedCommentByUserViewModel> LikedCommentsByUser { get; set; }
        = new();

    /// <summary>
    /// کامنت ها
    /// </summary>
    public List<ProductCommentForProductInfoViewModel> ProductComments { get; set; }
}