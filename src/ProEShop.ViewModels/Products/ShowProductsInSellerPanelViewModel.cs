using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProEShop.Common.Helpers;
using ProEShop.Entities;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.ViewModels.Products;

public class ShowProductsInSellerPanelViewModel
{
    public List<ShowProductInSellerPanelViewModel> Products { get; set; }

    public SearchProductsInSellerPanelViewModel SearchProducts { get; set; }
        = new();
    public PaginationViewModel Pagination { get; set; }
        = new();
}

public class ShowProductInSellerPanelViewModel
{
    [Display(Name = "شناسه")]
    public long Id { get; set; }

    [Display(Name = "عنوان فارسی")]
    public string PersianTitle { get; set; }

    [Display(Name = "تصویر محصول")]
    public string MainPicture { get; set; }

    [Display(Name = "نام فروشگاه")]
    public string SellerShopName { get; set; }

    [Display(Name = "برند محصول")]
    public string BrandFullTitle { get; set; }

    [Display(Name = "وضعیت محصول")]
    public ProductStatus Status { get; set; }

    [Display(Name = "دسته بندی اصلی")]
    public string CategoryTitle { get; set; }

    [Display(Name = "کد محصول")]
    public int ProductCode { get; set; }
}

public class SearchProductsInSellerPanelViewModel
{
    [EqualSearch]
    [Display(Name = "دسته بندی اصلی")]
    public long? MainCategoryId { get; set; }

    [EqualSearch]
    [Display(Name = "کد محصول")]
    public long? ProductCode { get; set; }

    public List<SelectListItem> Categories { get; set; }

    [ContainsSearch]
    [Display(Name = "عنوان فارسی")]
    [MaxLength(200)]
    public string PersianTitle { get; set; }

    [Display(Name = "وضعیت محصول")]
    public ProductStatus? Status { get; set; }

    [Display(Name = "وضعیت حذف شده ها")]
    public DeletedStatus DeletedStatus { get; set; }

    [Display(Name = "نمایش بر اساس")]
    public SortingProductsInSellerPanel Sorting { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    public SortingOrder SortingOrder { get; set; }
}

public enum SortingProductsInSellerPanel
{
    [Display(Name = "شناسه")]
    Id,

    [Display(Name = "عنوان")]
    PersianTitle,

    [Display(Name = "برند فارسی")]
    BrandFa,

    [Display(Name = "برند انگلیسی")]
    BrandEn
}