using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels;

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
    public int CurrentPage { get; set; } = 1;

    public byte Take { get; set; }

    public int PagesCount { get; set; }
}

public class PaginationResultViewModel<T>
{
    public IQueryable<T> Query { get; set; }

    public PaginationViewModel Pagination { get; set; }
}