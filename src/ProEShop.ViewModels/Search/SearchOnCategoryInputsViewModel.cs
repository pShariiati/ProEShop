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

    public List<long> Brands { get; set; }
}