namespace ProEShop.ViewModels.Orders;

/// <summary>
/// ویوو مدل نمایش سفارشات در بخش پروفایل کاربری
/// </summary>
public class ShowOrdersInProfileViewModel
{
    public List<ShowOrderInProfileViewModel> Orders { get; set; }

    public CommonPaginationViewModel Pagination { get; set; }
        = new();
}

public class ShowOrderInProfileViewModel
{
    public string CreatedDateTime { get; set; }

    public int FinalPrice { get; set; }

    public long OrderNumber { get; set; }

    public byte TotalScore { get; set; }

    public List<string> ProductImages { get; set; }

    public long ParcelPostItemsLongCount { get; set; }
}