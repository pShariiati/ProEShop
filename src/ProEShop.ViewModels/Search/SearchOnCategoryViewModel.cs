﻿using ProEShop.Entities;

namespace ProEShop.ViewModels.Search;

/// <summary>
/// ویوو مدل صفحه جستجو روی دسته بندی ها
/// </summary>
public class SearchOnCategoryViewModel
{
    /// <summary>
    /// برند های این دسته بندی
    /// </summary>
    public List<ShowBrandInSearchOnCategoryViewModel> CategoryBrands { get; set; }

    /// <summary>
    /// محصولات این دسته بندی
    /// </summary>
    public List<ShowProductInSearchOnCategoryViewModel> Products { get; set; }

    public List<ShowVariantInSearchOnCategoryViewModel> CategoryVariants { get; set; }

    public List<ShowFeatureInSearchOnCategoryViewModel> CategoryFeatures { get; set; }

    public long ProductsCount { get; set; }

    public int PagesCount => (int)Math.Ceiling(ProductsCount / (double)2);

    public bool? IsVariantColor { get; set; }

    /// <summary>
    /// قیمت ارزانترین محصول این دسته بندی
    /// </summary>
    public int MinimumPrice { get; set; }

    /// <summary>
    /// قیمت گرانترین محصول این دسته بندی
    /// </summary>
    public int MaximumPrice { get; set; }

    public List<BreadcrumbItemInSearchOnCategoryViewModel> BreadcrumbItems { get; set; }
}

public class BreadcrumbItemInSearchOnCategoryViewModel
{
    public string Title { get; set; }

    public string Slug { get; set; }
}

public class ShowBrandInSearchOnCategoryViewModel
{
    public string BrandTitleFa { get; set; }

    public string BrandTitleEn { get; set; }

    public long BrandId { get; set; }
}

public class ShowProductInSearchOnCategoryViewModel
{
    public bool IsMoreThanThreeColors { get; set; }

    public List<string> ColorCodes { get; set; }

    public bool IsFake { get; set; }

    public string Slug { get; set; }

    public int ProductCode { get; set; }

    public string PersianTitle { get; set; }

    public string Picture { get; set; }

    public double Score { get; set; }

    /// <summary>
    /// قیمت اصلی بدون تخفیف
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// قیمت نهایی
    /// اگه تخفیف داشته باشیم آف پرایس رو برگشت میزنه
    /// اگه تخفیف نداشته باشیم پرایس رو برگشت میزنه
    /// </summary>
    public int FinalPrice { get; set; }

    public byte? OffPercentage { get; set; }

    /// <summary>
    /// آخرین موجودی انبار
    /// اگه کمتر از سه باشه میگیم
    /// تنها کمتر از ایکس عدد در انبار باقی مانده است
    /// </summary>
    public byte LastInventoryCount { get; set; }

    public ProductStockStatus ProductStockStatus { get; set; }

    /// <summary>
    /// آیا تخفیف داریم
    /// </summary>
    public bool IsDiscountActive => OffPercentage != null;
}

public class ShowVariantInSearchOnCategoryViewModel
{
    public string VariantValue { get; set; }

    public string VariantColorCode { get; set; }

    public long VariantId { get; set; }
}

public class ShowFeatureInSearchOnCategoryViewModel
{
    public long FeatureId { get; set; }

    public string FeatureTitle { get; set; }

    public List<string> Values { get; set; }
}