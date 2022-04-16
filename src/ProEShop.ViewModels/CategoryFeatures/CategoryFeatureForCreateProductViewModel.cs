using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.CategoryFeatures;

public class CategoryFeatureForCreateProductViewModel
{
    public string FeatureTitle { get; set; }

    public long FeatureId { get; set; }
}