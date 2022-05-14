using System.ComponentModel.DataAnnotations;
using ProEShop.Common.Constants;
using ProEShop.Entities;
using ProEShop.ViewModels.Categories;

namespace ProEShop.ViewModels.Sellers;

public class ShowSellersViewModel
{
    public List<ShowSellerViewModel> Sellers { get; set; }

    public SearchSellersViewModel SearchSellers { get; set; }
        = new();
    public PaginationViewModel Pagination { get; set; }
        = new();
}

public class SearchSellersViewModel
{
    [Display(Name = "کد فروشنده")]
    public int? SellerCode { get; set; }

    [Display(Name = "نام فروشنده")]
    [MaxLength(500, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string FullName { get; set; }

    [Display(Name = "نام فروشگاه")]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string ShopName { get; set; }

    [Display(Name = "شخص حقوقی / شخص حقیقی")]
    public IsRealPersonStatus IsRealPersonStatus { get; set; }

    [Display(Name = "فعال / غیر فعال")]
    public IsActiveStatus IsActiveStatus { get; set; }

    [Display(Name = "وضعیت مدارک")]
    public DocumentStatus? DocumentStatus { get; set; }

    [Display(Name = "نمایش بر اساس")]
    public SortingSellers Sorting { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    public SortingOrder SortingOrder { get; set; }
}

public enum IsActiveStatus
{
    [Display(Name = "نمایش همه")]
    All,

    [Display(Name = "فعال")]
    Active,

    [Display(Name = "غیر فعال")]
    Disabled
}

public enum IsRealPersonStatus
{
    [Display(Name = "نمایش همه")]
    All,

    [Display(Name = "فقط اشخاص حقیقی")]
    IsRealPerson,

    [Display(Name = "فقط اشخاص حقوقی")]
    IsLegalPerson
}

public enum SortingSellers
{
    [Display(Name = "شناسه")]
    Id,

    [Display(Name = "کد فروشنده")]
    SellerCode,

    [Display(Name = "استان")]
    Province,

    [Display(Name = "شهرستان")]
    City,

    [Display(Name = "نام فروشنده")]
    FullName,

    [Display(Name = "نام فروشگاه")]
    ShopName,

    [Display(Name = "تاریخ ثبت نام")]
    CreatedDateTime
}

public class ShowSellerViewModel
{
    [Display(Name = "شماره همراه")]
    public string UserPhoneNumber { get; set; }

    [Display(Name = "شناسه")]
    public long Id { get; set; }

    [Display(Name = "شخص حقوقی / شخص حقیقی")]
    public bool IsRealPerson { get; set; }

    [Display(Name = "نام فروشگاه")]
    public string ShopName { get; set; }

    [Display(Name = "نام فروشنده")]
    public string UserFullName { get; set; }

    [Display(Name = "کد فروشنده")]
    public int SellerCode { get; set; }

    [Display(Name = "استان و شهرستان")]
    public string ProvinceAndCity { get; set; }

    [Display(Name = "وضعیت")]
    public DocumentStatus DocumentStatus { get; set; }

    [Display(Name = "فعال / غیر فعال")]
    public bool IsActive { get; set; }

    [Display(Name = "تاریخ ثبت نام")]
    public string CreatedDateTime { get; set; }
}