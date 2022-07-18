namespace ProEShop.ViewModels.Products;

public class ShowProductInfoViewModel
{
    public int ProductCode { get; set; }

    public string PersianTitle { get; set; }

    public string Slug { get; set; }

    public string EnglishTitle { get; set; }

    public string CategoryTitle { get; set; }

    public string BrandTitleFa { get; set; }

    public string BrandLogoPicture { get; set; }

    public string SellerShopName { get; set; }

    public string SellerLogo { get; set; }

    public string CategoryProductPageGuide { get; set; }

    public double Score { get; set; }

    /// <summary>
    /// تعداد کل رکورد های جدول کامنت محصول
    /// </summary>
    public long ProductCommentsLongCount { get; set; }

    /// <summary>
    /// تعداد نظراتی که برای محصول وجود دارد
    /// </summary>
    public long ProductCommentsCount { get; set; }

    public long SuggestCount { get; set; }

    public long BuyerCount { get; set; }

    public double SuggestPercentage
    {
        get
        {
            var divideResult = (double)BuyerCount / SuggestCount;
            return 100 / divideResult;
        }
    }

    public List<ProductMediaForProductInfoViewModel> ProductMedia { get; set; }

    public List<ProductCategoryForProductInfoViewModel> ProductCategories { get; set; }

    public List<ProductFeatureForProductInfoViewModel> ProductFeatures { get; set; }
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