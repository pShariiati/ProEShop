using System.ComponentModel.DataAnnotations;
using ProEShop.Common.Helpers;

namespace ProEShop.ViewModels.Guarantees;

public class ShowGuaranteesViewModel
{
    public List<ShowGuaranteeViewModel> Guarantees { get; set; }

    public SearchGuaranteesViewModel SearchGuarantees { get; set; }
        = new();
    public PaginationViewModel Pagination { get; set; }
        = new();
}

public class ShowGuaranteeViewModel
{
    [Display(Name = "شناسه")]
    public long Id { get; set; }

    [Display(Name = "عنوان کامل گارانتی")]
    public string FullTitle { get; set; }

    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [Display(Name = "وضعیت")]
    public bool IsConfirmed { get; set; }

    [Display(Name = "تصویر")]
    public string Picture { get; set; }

    [Display(Name = "تعداد ماه گارانتی")]
    public byte MonthsCount { get; set; }

    [Display(Name = "نمایش بر اساس")]
    public SortingGuarantees Sorting { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    public SortingOrder SortingOrder { get; set; }
}

public class SearchGuaranteesViewModel
{
    [ContainsSearch]
    [Display(Name = "عنوان")]
    public string Title { get; set; }
    
    [EqualSearch]
    [Display(Name = "تعداد ماه گارانتی")]
    public byte? MonthsCount { get; set; }

    [EqualSearch]
    [Display(Name = "وضعیت")]
    public bool? IsConfirmed { get; set; }

    [Display(Name = "نمایش بر اساس")]
    public SortingGuarantees Sorting { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    public SortingOrder SortingOrder { get; set; }
}

public enum SortingGuarantees
{
    [Display(Name = "شناسه")]
    Id,

    [Display(Name = "عنوان")]
    Title,

    [Display(Name = "تعداد ماه گارانتی")]
    MonthsCount
}