namespace ProEShop.ViewModels.Products;

public class ShowProductInCompareViewModel
{
    public int ProductCode { get; set; }

    public string Slug { get; set; }

    public string MainPicture { get; set; }

    public string PersianTitle { get; set; }

    public double Score { get; set; }

    public int Price { get; set; }

    public List<ShowFeatureInCompareViewModel> ProductFeatures { get; set; }
}

public class ShowFeatureInCompareViewModel
{
    public string Value { get; set; }

    public string FeatureTitle { get; set; }
}