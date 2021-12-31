using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.CategoryFeatures;

public class ShowCategoryFeaturesViewModel
{
    public List<ShowCategoryFeatureViewModel> Categories { get; set; }

    public PaginationViewModel Pagination { get; set; }
    = new();
}

public class ShowCategoryFeatureViewModel
{
    [Display(Name = "شناسه")]
    public long Id { get; set; }

    [Display(Name = "عنوان")]
    public string Title { get; set; }

    public bool IsDeleted { get; set; }
}

public enum SortingFeatures
{
    [Display(Name = "شناسه")]
    Id,

    [Display(Name = "عنوان")]
    ShowInMenusStatus,

    [Display(Name = "حذف شده ها")]
    IsDeleted
}