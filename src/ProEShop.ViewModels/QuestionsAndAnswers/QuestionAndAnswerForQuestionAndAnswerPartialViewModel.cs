using ProEShop.ViewModels.Products;

namespace ProEShop.ViewModels.QuestionsAndAnswers;

/// <summary>
/// ویوو مدل پارشلِ سوال و جواب های صفحه تکی محصول
/// </summary>
public class QuestionAndAnswerForQuestionAndAnswerPartialViewModel
{
    /// <summary>
    /// صفحه جاری
    /// داخل چه صفحه ایی هستیم ؟
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// تعداد کل صفحات
    /// </summary>
    public int QuestionsAndAnswersPagesCount { get; set; }

    /// <summary>
    /// از داخل کل جواب هایی که نمایش داده میشود
    /// کدام یک توسط این کاربر لایک و یا دیسلایک شده است ؟
    /// اگر کاربر وارد سیستم نباشه این مورد نال میشه و دیگه نمیتونیم داخل
    /// پارشل از سینگل اور دیفالت استفاده کنیم پس یک مقدار پیش فرض رو براش
    /// در نظر میگیریم که اگر نال بود هنگامی که از سینگل اور دیفالت استفاده میکنیم
    /// دچار خطا نشویم، چون نمیوان روی یک شیء نال از سینگل اور دیفالت استفاده کرد
    /// </summary>
    public List<LikedAnswerByUserViewModel> LikedAnswersByUser { get; set; }
        = new();

    /// <summary>
    /// سوالات و جواب ها
    /// </summary>
    public List<ProductQuestionForProductInfoViewModel> ProductQuestionsAndAnswers { get; set; }
}