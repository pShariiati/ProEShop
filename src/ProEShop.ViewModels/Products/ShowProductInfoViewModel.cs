namespace ProEShop.ViewModels.Products;

/// <summary>
/// نمایش اطلاعات محصول در صفحه تکی محصول
/// </summary>
public class ShowProductInfoViewModel
{
    public long Id { get; set; }

    public bool IsVariantTypeNull { get; set; }

    public bool IsFavorite { get; set; }

    public int ProductCode { get; set; }

    public string PersianTitle { get; set; }

    public string Slug { get; set; }

    public string EnglishTitle { get; set; }

    public string CategoryTitle { get; set; }

    public string BrandTitleFa { get; set; }

    public string BrandLogoPicture { get; set; }

    public string CategoryProductPageGuide { get; set; }

    /// <summary>
    /// امتیاز محصول
    /// از پنج
    /// </summary>
    public double Score { get; set; }

    /// <summary>
    /// تعداد کل رکورد های جدول کامنت محصول
    /// چه تایید شده چه نشده
    /// نیاز به فور ممبر ندارد، قوانین توکار خود اتو مپر این پراپرتی رو مقدار دهی میکنه
    /// </summary>
    public long ProductCommentsLongCount { get; set; }

    /// <summary>
    /// تعداد نظراتی که برای محصول وجود دارد
    /// فقط تایید شده ها و آنهایی که متن نظر دارند
    /// </summary>
    public long ProductCommentsCount { get; set; }

    /// <summary>
    /// تعداد سوالات تایید شده این محصول
    /// </summary>
    public long ProductQuestionsCount { get; set; }

    /// <summary>
    /// چند نفر از خریداران ای کالا را پیشنهاد کرده اند
    /// </summary>
    public long SuggestCount { get; set; }

    /// <summary>
    /// تعداد کل خریداران
    /// </summary>
    public long BuyerCount { get; set; }

    /// <summary>
    /// چند درصد از خریداران این کالا را پیشنهاد کرده اند
    /// </summary>
    public double SuggestPercentage
    {
        get
        {
            var divideResult = (double)BuyerCount / SuggestCount;
            return 100 / divideResult;
        }
    }

    public string ShortDescription { get; set; }

    public string SpecialtyCheck { get; set; }

    public string ProductShortLinkDisplayLink { get; set; }

    /// <summary>
    /// نظرات این محصول در چند صفحه نمایش داده میشوند
    /// </summary>
    public int CommentsPagesCount { get; set; }

    /// <summary>
    /// سوالات این محصول در چند صفحه نمایش داده میشوند
    /// </summary>
    public int QuestionsPagesCount { get; set; }

    public List<ProductMediaForProductInfoViewModel> ProductMedia { get; set; }

    public List<ProductCategoryForProductInfoViewModel> ProductCategories { get; set; }

    public List<ProductFeatureForProductInfoViewModel> ProductFeatures { get; set; }

    public List<ProductVariantForProductInfoViewModel> ProductVariants { get; set; }

    /// <summary>
    /// تنوع های این محصول که در سبد خرید این کاربری که، صفحه رو لود میکنه قرار داره
    /// </summary>
    public List<ProductVariantInCartForProductInfoViewModel> ProductVariantsInCart { get; set; }

    public List<ProductCommentForProductInfoViewModel> ProductComments { get; set; }

    public List<ProductQuestionForProductInfoViewModel> ProductsQuestionsAndAnswers { get; set; }

    public List<LikedCommentByUserViewModel> LikedCommentsByUser { get; set; }

    public List<LikedAnswerByUserViewModel> LikedAnswersByUser { get; set; }
}

public class ProductVariantInCartForProductInfoViewModel
{
    public long ProductVariantId { get; set; }

    public short Count { get; set; }
}

public class ProductMediaForProductInfoViewModel
{
    public string FileName { get; set; }

    public bool IsVideo { get; set; }
}

public class ProductCategoryForProductInfoViewModel
{
    public string CategorySlug { get; set; }

    public string CategoryTitle { get; set; }
}

public class ProductFeatureForProductInfoViewModel
{
    public string FeatureTitle { get; set; }

    public string Value { get; set; }

    public bool FeatureShowNextToProduct { get; set; }
}

public class ProductVariantForProductInfoViewModel
{
    public long Id { get; set; }

    public string VariantValue { get; set; }

    public string VariantColorCode { get; set; }

    public int Price { get; set; }

    public int FinalPrice { get; set; }

    public byte? OffPercentage { get; set; }

    public string SellerShopName { get; set; }

    public string SellerLogo { get; set; }

    public string GuaranteeFullTitle { get; set; }

    public string EndDateTime { get; set; }

    public bool IsDiscountActive { get; set; }

    public byte Count { get; set; }

    public short MaxCountInCart { get; set; }

    public byte Score
    {
        get
        {
            var result = Math.Ceiling((double)Price / 10000);
            if (result <= 1)
                return 1;
            if (result >= 150)
                return 150;
            return (byte)result;
        }
    }
}

public class ProductCommentForProductInfoViewModel
{
    public long Id { get; set; }

    /// <summary>
    /// آیا این نظر توسط  یک فروشگاه ثبت شده است
    /// </summary>
    public bool IsShop { get; set; }

    /// <summary>
    /// نام کس یا فروشگاهی که کامنت رو ثبت کرده
    /// </summary>
    public string Name { get; set; }

    public byte Score { get; set; }

    public string CommentTitle { get; set; }

    public string CreatedDateTime { get; set; }

    public bool IsBuyer { get; set; }

    public bool? Suggest { get; set; }

    public string PositiveItems { get; set; }

    public string NegativeItems { get; set; }

    public string CommentText { get; set; }

    public string SellerShopNameShopName { get; set; }

    public long Like { get; set; }

    public long Dislike { get; set; }

    public string VariantColorCode { get; set; }

    public bool? VariantIsColor { get; set; }

    public string VariantValue { get; set; }
}

/// <summary>
/// ویوو مدل برای کامنت هایی که توسط کاربر داخل سیستم لایک و دیس لایک شده اند
/// </summary>
public class LikedCommentByUserViewModel
{
    public long ProductCommentId { get; set; }

    public bool IsLike { get; set; }
}

/// <summary>
/// ویوو مدل برای جواب های سوالاتی که توسط کاربر داخل سیستم لایک و دیس لایک شده اند
/// </summary>
public class LikedAnswerByUserViewModel
{
    public long AnswerId { get; set; }

    public bool IsLike { get; set; }
}

/// <summary>
/// سوالات محصول
/// </summary>
public class ProductQuestionForProductInfoViewModel
{
    /// <summary>
    /// متن سوال
    /// </summary>
    public string Body { get; set; }

    public List<ProductQuestionAnswerForProductInfoViewModel> Answers { get; set; }
}

/// <summary>
/// جواب های پرسش های محصول
/// </summary>
public class ProductQuestionAnswerForProductInfoViewModel
{
    public long Id { get; set; }

    /// <summary>
    /// آیا این پاسخ توسط یک فروشگاه به ثبت رسیده است ؟
    /// </summary>
    public bool IsShop { get; set; }

    /// <summary>
    /// متن پاسخ
    /// </summary>
    public string Body { get; set; }

    public bool IsBuyer { get; set; }

    public string Name { get; set; }

    public long Like { get; set; }

    public long Dislike { get; set; }
}