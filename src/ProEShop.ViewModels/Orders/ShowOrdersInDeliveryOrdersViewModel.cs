using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Entities.Enums;

namespace ProEShop.ViewModels.Orders;

/// <summary>
/// نمایش سفارشات در بخش تحویل دادن مرسوله
/// </summary>
public class ShowOrdersInDeliveryOrdersViewModel
{
    public List<ShowOrderInDeliveryOrdersViewModel> Orders { get; set; }

    public SearchOrdersInDeliveryOrdersViewModel SearchOrders { get; set; }
        = new();

    public PaginationViewModel Pagination { get; set; }
        = new();

    public List<SelectListItem> Provinces { get; set; }
}

public class ShowOrderInDeliveryOrdersViewModel
{
    public long Id { get; set; }

    [Display(Name = "شماره سفارش")]
    public long OrderNumber { get; set; }

    [Display(Name = "تاریخ ایجاد")]
    public string CreatedDateTime { get; set; }

    [Display(Name = "گیرنده")]
    public string AddressFullName { get; set; }

    [Display(Name = "مقصد")]
    public string Destination { get; set; }

    [Display(Name = "وضعیت")]
    public OrderStatus Status { get; set; }

    public List<ShowParcelPostInDeliveryOrdersViewModel> ParcelPosts { get; set; }
}

public class ShowParcelPostInDeliveryOrdersViewModel
{
    public long Id { get; set; }

    public Dimension Dimension { get; set; }

    public ParcelPostStatus Status { get; set; }
}

public class SearchOrdersInDeliveryOrdersViewModel
{
    [Display(Name = "شماره سفارش")]
    [ContainsSearch]
    [LtrDirection]
    public string OrderNumber { get; set; }

    [Display(Name = "نمایش بر اساس")]
    public SortingOrders Sorting { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    public SortingOrder SortingOrder { get; set; } = SortingOrder.Desc;

    [Display(Name = "گیرنده")]
    [MaxLength(400, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$",
        ErrorMessage = "لطفا تنها از حروف فارسی استفاده نمائید")]
    public string FullName { get; set; }

    [Display(Name = "تاریخ ایجاد")]
    [EqualDateTimeSearch]
    [RegularExpression(@"^[۰-۹]{4}\/(۰[۱-۹]|۱[۰-۲])\/(۰[۱-۹]|[۱۲][۰-۹]|۳[۰۱])$", ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public string CreatedDateTime { get; set; }

    [Display(Name = "استان")]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public long? ProvinceId { get; set; }

    [Display(Name = "شهرستان")]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public long? CityId { get; set; }

    [Display(Name = "وضعیت")]
    [EnumEqualSearch]
    public OrderStatusInDeliveryOrders? Status { get; set; }
}

public enum OrderStatusInDeliveryOrders : byte
{
    [Display(Name = "پردازش انبار")]
    InventoryProcessing = 2,

    [Display(Name = "بخشی از مرسوله ها در پست")]
    SomeParcelsDeliveredToPost,

    [Display(Name = "تمام مرسوله ها در پست")]
    CompletelyParcelsDeliveredToPost,

    [Display(Name = "تحویل شده")]
    DeliveredToClient
}