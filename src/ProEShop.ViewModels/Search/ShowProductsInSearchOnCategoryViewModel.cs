namespace ProEShop.ViewModels.Search;

/// <summary>
/// برای پارشل صفحه جستجو
/// </summary>
public class ShowProductsInSearchOnCategoryViewModel
{
    public List<ShowProductInSearchOnCategoryViewModel> Products { get; set; }

    /// <summary>
    /// تعداد کل صفحات برای بخش صفحه بندی
    /// </summary>
    public int PagesCount { get; set; }

    /// <summary>
    /// در چه صفحه ایی هستیم که اون صفحه رو اکتیو کنیم
    /// </summary>
    public int CurrentPage { get; set; }

    public long AllProductsCount { get; set; }
}