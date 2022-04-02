using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.Brands;

public class BrandDetailsViewModel
{
    public long Id { get; set; }

    [Display(Name = "نام فارسی برند")]
    public string TitleFa { get; set; }

    [Display(Name = "نام انگلیسی برند")]
    public string TitleEn { get; set; }

    [Display(Name = "شرح برند")]
    public string Description { get; set; }

    [Display(Name = "نوع برند")]
    public bool IsIranianBrand { get; set; }

    [Display(Name = "لوگوی برند")]
    public string LogoPicture { get; set; }

    [Display(Name = "برگه ثبت برند")]
    public string BrandRegistrationPicture { get; set; }

    [Display(Name = "لینک سایت قوه قضاییه")]
    public string JudiciaryLink { get; set; }

    [Display(Name = "لینک سایت معتبر خارجی")]
    public string BrandLinkEn { get; set; }
    
    public bool IsConfirmed { get; set; }

    public string SellerShopName { get; set; }

    public string SellerUserFullName { get; set; }
}