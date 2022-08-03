using System.ComponentModel.DataAnnotations;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;

namespace ProEShop.ViewModels.ProductShortLinks;

public class ShowProductShortLinksViewModel
{
    public List<ShowProductShortLinkViewModel> ProductShortLinks { get; set; }

    public SearchProductShortLinksViewModel SearchBrands { get; set; }
        = new();

    public PaginationViewModel Pagination { get; set; }
        = new();
}

public class ShowProductShortLinkViewModel
{
    [Display(Name = "شناسه")]
    public long Id { get; set; }

    [Display(Name = "آدرس لینک")]
    public string DisplayLink { get; set; }

    [Display(Name = "وضعیت")]
    public bool IsUsed { get; set; }
}

public class SearchProductShortLinksViewModel
{
    [ContainsSearch]
    [Display(Name = "آدرس لینک")]
    [MaxLength(10, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string DisplayLink { get; set; }

    [EqualSearch]
    [Display(Name = "وضعیت")]
    public bool? IsUsed { get; set; }

    [Display(Name = "نمایش بر اساس")]
    public SortingProductShortLinks Sorting { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    public SortingOrder SortingOrder { get; set; }
}

public enum SortingProductShortLinks
{
    [Display(Name = "شناسه")]
    Id,

    [Display(Name = "آدرس لینک")]
    DisplayLink
}