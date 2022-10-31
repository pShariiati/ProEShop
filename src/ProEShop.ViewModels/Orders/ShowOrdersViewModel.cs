using ProEShop.ViewModels.Brands;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Entities.Enums;

namespace ProEShop.ViewModels.Orders;

/// <summary>
/// نمایش سفارشات
/// </summary>
public class ShowOrdersViewModel
{
    public List<ShowOrderViewModel> Orders { get; set; }

    public SearchOrdersViewModel SearchOrders { get; set; }
        = new();

    public PaginationViewModel Pagination { get; set; }
        = new();

    public List<SelectListItem> Provinces { get; set; }
}

public class ShowOrderViewModel
{
    [Display(Name = "شماره سفارش")]
    public long OrderNumber { get; set; }

    [Display(Name = "تاریخ ایجاد")]
    public string CreatedDateTime { get; set; }

    [Display(Name = "گیرنده")]
    public string AddressFullName { get; set; }

    [Display(Name = "مقصد")]
    public string Destination { get; set; }

    [Display(Name = "درگاه")]
    public PaymentGateway? PaymentGateway { get; set; }
}

public class SearchOrdersViewModel
{
    [Display(Name = "شماره سفارش")]
    [EqualSearch]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public long? OrderNumber { get; set; }

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
    [RegularExpression(@"^[۰-۹]{4}\/(۰[۱-۹]|۱[۰-۲])\/(۰[۱-۹]|[۱۲][۰-۹]|۳[۰۱])$", ErrorMessage = "سن باید بین ۱۸ و ۱۰۰ سال باشد")]
    public string CreatedDateTime { get; set; }

    [Display(Name = "استان")]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public long? ProvinceId { get; set; }

    [Display(Name = "شهرستان")]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public long? CityId { get; set; }

    [Display(Name = "درگاه")]
    public PaymentGateway? PaymentGateway { get; set; }
}

public enum SortingOrders
{
    [Display(Name = "شماره سفارش")]
    OrderNumber,

    [Display(Name = "تاریخ ایجاد")]
    CreatedDateTime
}