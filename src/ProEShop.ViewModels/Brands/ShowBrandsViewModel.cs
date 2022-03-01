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
}

public class SearchBrandsViewModel
{
    [Display(Name = "نام فارسی برند")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string TitleFa { get; set; }
}