namespace ProEShop.ViewModels.Products;

/// <summary>
/// ویو مدل نمایش محصولات در بخش کامنت ها و بخش پروفایل کاربری
/// </summary>
public class ShowProductsInProfileCommentViewModel
{
    public List<ShowProductInProfileCommentViewModel> Items { get; set; }

    public CommonPaginationViewModel Pagination { get; set; }
        = new();
}

public class ShowProductInProfileCommentViewModel
{
    public string Title { get; set; }

    public string Picture { get; set; }
}