using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;
using ProEShop.Entities;

namespace ProEShop.ViewModels.Products;

public class ProductDetailsViewModel
{
    [HiddenInput]
    public long Id { get; set; }

    [Display(Name = "دلیل رد شدن محصول")]
    [MakeTinyMceRequired]
    public string RejectReason { get; set; }

    [Display(Name = "نام فارسی محصول")]
    public string PersianTitle { get; set; }

    [Display(Name = "نام انگلیسی محصول")]
    public string EnglishTitle { get; set; }
    
    public string Slug { get; set; }

    public int ProductCode { get; set; }

    public bool IsFake { get; set; }

    [Display(Name = "وزن بسته بندی")]
    public int PackWeight { get; set; }

    [Display(Name = "طول بسته بندی")]
    public int PackLength { get; set; }

    [Display(Name = "عرض بسته بندی")]
    public int PackWidth { get; set; }

    [Display(Name = "ارتفاع بسته بندی")]
    public int PackHeight { get; set; }

    [Display(Name = "توضیحات مختصر محصول")]
    public string ShortDescription { get; set; }

    [Display(Name = "بررسی تخصصی محصول")]
    public string SpecialtyCheck { get; set; }

    [Display(Name = "نام فروشگاه")]
    public string SellerShopName { get; set; }

    [Display(Name = "برند محصول")]
    public string BrandFullTitle { get; set; }

    public ProductStatus Status { get; set; }

    public string CategoryTitle { get; set; }

    public List<ProductMediaForProductDetailsViewModel> ProductMedia { get; set; }

    public List<ProductFeatureForProductDetailsViewModel> ProductFeatures { get; set; }
}