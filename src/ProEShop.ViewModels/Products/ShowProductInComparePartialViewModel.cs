namespace ProEShop.ViewModels.Products;

/// <summary>
/// نمایش محصولات داخل پارشل افزودن محصول صفحه مقایسه
/// </summary>
public class ShowProductInComparePartialViewModel
{
    /// <summary>
    /// تعداد کل محصولات
    /// </summary>
    public long Count { get; set; }

    public List<ProductItemForShowProductInComparePartialViewModel> Products { get; set; }
}

public class ProductItemForShowProductInComparePartialViewModel
{
    public string Slug { get; set; }

    public int ProductCode { get; set; }

    public string MainPicture { get; set; }

    public string PersianTitle { get; set; }

    public double Score { get; set; }

    public int Price { get; set; }

    public byte Count { get; set; }
}