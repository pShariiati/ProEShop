using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProEShop.Common.Constants;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;

namespace ProEShop.ViewModels.Categories;

public class AddBrandToCategoryViewModel
{
    [HiddenInput]
    public long SelectedCategoryId { get; set; }

    public List<string> SelectedBrands { get; set; }
        = new();
}
