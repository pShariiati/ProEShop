using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProEShop.Common.Constants;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;

namespace ProEShop.ViewModels.Categories;

public class EditCategoryViewModel : AddCategoryViewModel
{
    [HiddenInput]
    public long Id { get; set; }

    [Display(Name = "تصویر انتخاب شده")]
    public string SelectedPicture { get; set; }
}
