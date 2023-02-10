using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ProEShop.ViewModels;

public class ShowSelect2DataByAjaxViewModel
{
    public long Id { get; set; }

    public string Text { get; set; }
}

public enum PageCount
{
    [Display(Name = "۱۰ سطر")]
    Ten,

    [Display(Name = "۲۵ سطر")]
    TwentyFive,

    [Display(Name = "۵۰ سطر")]
    Fifty,

    [Display(Name = "۱۰۰ سطر")]
    Hundred
}

public enum SortingOrder
{
    [Display(Name = "صعودی")]
    Asc,

    [Display(Name = "نزولی")]
    Desc
}

public static class ViewModelConstants
{
    public const string AntiForgeryToken = "__RequestVerificationToken";
}

public enum DeletedStatus
{
    [Display(Name = "نمایش داده نشوند")]
    False,
    [Display(Name = "نمایش داده شوند")]
    True,
    [Display(Name = "فقط حذف شده ها")]
    OnlyDeleted
}

public class PaginationViewModel
{
    [HiddenInput]
    public int CurrentPage { get; set; } = 1;

    [HiddenInput]
    public PageCount PageCount { get; set; } = PageCount.Ten;

    public int PagesCount { get; set; }

    public int StartPage => CurrentPage - 3 < 1 ? 1 : CurrentPage - 3;

    public int EndPage => CurrentPage + 3 > PagesCount ? PagesCount : CurrentPage + 3;
}

public class CommonPaginationViewModel
{
    public int CurrentPage { get; set; } = 1;

    public int PagesCount { get; set; }

    public int StartPage => CurrentPage - 3 < 1 ? 1 : CurrentPage - 3;

    public int EndPage => CurrentPage + 3 > PagesCount ? PagesCount : CurrentPage + 3;

    public string FunctionName { get; set; }
}

public class PaginationResultViewModel<T>
{
    public IQueryable<T> Query { get; set; }

    public PaginationViewModel Pagination { get; set; }
}

public class CommonPaginationResultViewModel<T>
{
    public IQueryable<T> Query { get; set; }

    public CommonPaginationViewModel Pagination { get; set; }
}