using System.ComponentModel.DataAnnotations;
using ProEShop.Common.Helpers;
using ProEShop.Entities;

namespace ProEShop.ViewModels.Consignments;

public class ShowConsignmentsViewModel
{
    public List<ShowConsignmentViewModel> Consignments { get; set; }

    public SearchConsignmentsViewModel SearchConsignments { get; set; }
        = new();

    public PaginationViewModel Pagination { get; set; }
        = new();
}

public class ShowConsignmentViewModel
{
    [Display(Name = "شناسه")]
    public long Id { get; set; }

    [Display(Name = "نام فروشگاه")]
    public string SellerShopName { get; set; }

    [Display(Name = "تاریخ تحویل")]
    public string DeliveryDate { get; set; }

    [Display(Name = "توضیحات")]
    public string Description { get; set; }

    [Display(Name = "وضعیت محموله")]
    public ConsignmentStatus ConsignmentStatus { get; set; }
}

public class SearchConsignmentsViewModel
{
    [Display(Name = "نام فروشگاه")]
    public string ShopName { get; set; }

    [Display(Name = "تاریخ تحویل")]
    [EqualDateTimeSearch]
    public string DeliveryDate { get; set; }

    [EqualSearch]
    [Display(Name = "وضعیت محموله")]
    public ConsignmentStatus? ConsignmentStatus { get; set; }

    [Display(Name = "نمایش بر اساس")]
    public SortingConsignments Sorting { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    public SortingOrder SortingOrder { get; set; }
}

public enum SortingConsignments
{
    [Display(Name = "شناسه")]
    Id,

    [Display(Name = "تاریخ تحویل")]
    DeliveryDate,

    [Display(Name = "نام فروشگاه")]
    ShopName
}