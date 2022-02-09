using System.ComponentModel.DataAnnotations;
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

    [Display(Name = "نمایش بر اساس")]
    public SortingSellers Sorting { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    public SortingOrder SortingOrder { get; set; }
}


public enum SortingSellers
{
    [Display(Name = "شناسه")]
    Id
}

public class ShowSellerViewModel
{
    [Display(Name = "نام فروشگاه")]
    public string ShopName { get; set; }

    [Display(Name = "نام فروشنده")]
    public string FullName { get; set; }

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