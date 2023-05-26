namespace ProEShop.ViewModels.Search;

/// <summary>
/// موقعی که در صفحه جستجو دسته بندی ها، یک درخواست رو به سمت سرور میفرستیم این اطلاعات رو به سمت سرور ارسال میکنیم
/// </summary>
public class SearchOnCategoryInputsViewModel
{
    /// <summary>
    /// چه صفحه ایی رو لود کنه
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// محصولات کدوم دسته بندی رو بخونه
    /// </summary>
    public string CategorySlug { get; set; }

    /// <summary>
    /// برند های انتخاب شده از سمت راست صفحه
    /// </summary>
    public List<long> Brands { get; set; }

    /// <summary>
    /// تنوع های انتخاب شده از سمت راست صفحه
    /// </summary>
    public List<long> Variants { get; set; }

    /// <summary>
    /// کمترین قیمت برای فیلتر کردن محصولات
    /// </summary>
    public int MinimumPrice { get; set; }

    /// <summary>
    /// بیشترین قیمت برای فیلتر کردن محصولات
    /// </summary>
    public int MaximumPrice { get; set; }

    /// <summary>
    /// فقط کالا هایی که در داخل انبار موجود هستند
    /// </summary>
    public bool OnlyExistsProducts { get; set; }

    /// <summary>
    /// فیچر هایی که تیکشون فعال شده
    /// </summary>
    public List<string> Features { get; set; }

    public SortingSearchOnCategory Sorting { get; set; }

    public SortingOrder SortingOrder { get; set; }
}

public enum SortingSearchOnCategory
{
    /// <summary>
    /// تعداد مشاهده
    /// </summary>
    VisitCount,
    /// <summary>
    /// جدیدترین
    /// </summary>
    Id,
    /// <summary>
    /// پر فروشترین
    /// </summary>
    SaleCount,
    /// <summary>
    /// ارزانترین
    /// گرانترین
    /// </summary>
    Price,
    /// <summary>
    /// پیشنهاد خریداران
    /// </summary>
    BuyersSuggest
}