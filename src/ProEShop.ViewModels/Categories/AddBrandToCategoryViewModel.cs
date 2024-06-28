using Microsoft.AspNetCore.Mvc;

namespace ProEShop.ViewModels.Categories;

public class AddBrandToCategoryViewModel
{
    [HiddenInput]
    public long SelectedCategoryId { get; set; }

    public List<string> SelectedBrands { get; set; }
        = new();
}
