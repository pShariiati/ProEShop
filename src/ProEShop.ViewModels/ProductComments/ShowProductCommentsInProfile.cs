namespace ProEShop.ViewModels.ProductComments;

/// <summary>
/// نمایش نظرات کاربر در بخش پروفایل کاربری
/// </summary>
public class ShowProductCommentsInProfile
{
    public List<ShowProductCommentInProfile> Items { get; set; }

    public PaginationViewModel Pagination { get; set; }
        = new();
}

public class ShowProductCommentInProfile
{
    public string ProductPersianTitle { get; set; }

    public int ProductProductCode { get; set; }

    public string ProductSlug { get; set; }

    public string MainPicture { get; set; }

    public string CommentTitle { get; set; }

    public string CommentText { get; set; }

    public byte Score { get; set; }

    /// <summary>
    /// اگر نال باشه یعنی نظر در حال بررسی است
    /// </summary>
    public bool? IsConfirmed { get; set; }

    public bool? Suggest { get; set; }

    /// <summary>
    /// اگر نال باشه یعنی کاربر این محصول رو خریداری نکرده
    /// </summary>
    public string SellerShopNameShopName { get; set; }

    /// <summary>
    /// اگر نال باشه یعنی این محصول تنوع نداره
    /// </summary>
    public string VariantValue { get; set; }

    public bool VariantIsColor { get; set; }

    public string VariantColorCode { get; set; }

    /// <summary>
    /// آیا این نظر توسط فروشگاهِ کاربر به ثبت رسیده است ؟
    /// </summary>
    public bool IsSeller { get; set; }

    /// <summary>
    /// تعداد لایک های نظر
    /// </summary>
    public long Like { get; set; }
}