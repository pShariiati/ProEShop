using System.ComponentModel.DataAnnotations;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.Brands;

public class ShowBrandsViewModel
{
    public List<ShowBrandViewModel> Brands { get; set; }

    public SearchBrandsViewModel SearchBrands { get; set; }
        = new();

    public PaginationViewModel Pagination { get; set; }
        = new();
}

public class ShowBrandViewModel
{
    [Display(Name = "شناسه")]
    public long Id { get; set; }

    [Display(Name = "نام فارسی برند")]
    public string TitleFa { get; set; }

    [Display(Name = "نام انگلیسی برند")]
    public string TitleEn { get; set; }

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
}

public class SearchBrandsViewModel
{
    [Display(Name = "نام فارسی برند")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string TitleFa { get; set; }

    [Display(Name = "وضعیت حذف شده ها")]
    public DeletedStatus DeletedStatus { get; set; }

    [Display(Name = "نمایش بر اساس")]
    public SortingBrands Sorting { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    public SortingOrder SortingOrder { get; set; }
}

public enum SortingBrands
{
    [Display(Name = "شناسه")]
    Id
}