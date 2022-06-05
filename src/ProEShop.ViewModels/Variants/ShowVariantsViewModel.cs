using System.ComponentModel.DataAnnotations;
using ProEShop.Common.Helpers;

namespace ProEShop.ViewModels.Variants;

public class ShowVariantsViewModel
{
    public List<ShowVariantViewModel> Variants { get; set; }

    public SearchVariantsViewModel SearchVariants { get; set; }
        = new();
    public PaginationViewModel Pagination { get; set; }
        = new();
}

public class ShowVariantViewModel
{
    [Display(Name = "شناسه")]
    public long Id { get; set; }

    [Display(Name = "مقدار")]
    public string Value { get; set; }

    [Display(Name = "کد رنگ")]
    public string ColorCode { get; set; }

    [Display(Name = "رنگ / اندازه")]
    public bool IsColor { get; set; }
}

public class SearchVariantsViewModel
{
    [ContainsSearch]
    [Display(Name = "مقدار")]
    public string Value { get; set; }

    [ContainsSearch]
    [Display(Name = "کد رنگ")]
    public string ColorCode { get; set; }

    [EqualSearch]
    [Display(Name = "رنگ / اندازه")]
    public bool? IsColor { get; set; }

    [Display(Name = "نمایش بر اساس")]
    public SortingVariants Sorting { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    public SortingOrder SortingOrder { get; set; }
}

public enum SortingVariants
{
    [Display(Name = "شناسه")]
    Id,

    [Display(Name = "مقدار")]
    Value,
}