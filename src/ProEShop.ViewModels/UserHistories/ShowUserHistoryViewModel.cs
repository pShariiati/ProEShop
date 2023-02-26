namespace ProEShop.ViewModels.UserHistories;

/// <summary>
/// نمایش محصولات در بخش بازدید های اخیر
/// </summary>
public class ShowUserHistoryViewModel
{
    public long ProductId { get; set; }

    public int ProductProductCode { get; set; }

    public string ProductSlug { get; set; }

    public string ProductImage { get; set; }

    public string ProductPersianTitle { get; set; }

    public int FinalPrice { get; set; }

    public int Price { get; set; }

    public byte? OffPercentage { get; set; }

    public short CountInCart { get; set; }
}