using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Features;

namespace ProEShop.Web.Pages.Admin.Feature;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IFeatureService _featureService;
    private readonly ICategoryService _categoryService;
    private readonly ICategoryFeatureService _categoryFeatureService;
    private readonly IUnitOfWork _uow;

    public IndexModel(
        IFeatureService featureService,
        IUnitOfWork uow,
        ICategoryService categoryService,
        ICategoryFeatureService categoryFeatureService)
    {
        _featureService = featureService;
        _uow = uow;
        _categoryService = categoryService;
        _categoryFeatureService = categoryFeatureService;
    }

    #endregion

    public ShowFeaturesViewModel Features { get; set; }
        = new();

    public async Task OnGet()
    {
        var categories = await _categoryService.GetCategoriesToShowInSelectBoxAsync();
        Features.SearchFeatures.Categories = categories.CreateSelectListItem();
    }

    public async Task<IActionResult> OnPostAddAsync(AddFeatureViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        var searchedTitle = model.Title.Trim();

        var feature = await _featureService.FindByTitleAsync(searchedTitle);
        if (feature is null)
        {
            await _featureService.AddAsync(new Entities.Feature()
            {
                Title = searchedTitle,
                CategoryFeatures = new List<CategoryFeature>()
                {
                    new CategoryFeature()
                    {
                        CategoryId = model.CategoryId
                    }
                }
            });
        }
        else
        {
            var categoryFeature = await _categoryFeatureService
                .GetCategoryFeature(model.CategoryId, feature.Id);
            if (categoryFeature is null)
            {
                feature.CategoryFeatures.Add(new CategoryFeature()
                {
                    CategoryId = model.CategoryId
                });
            }
        }
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "ویژگی دسته بندی مورد نظر با موفقیت اضافه شد"));
    }

    public async Task<IActionResult> OnGetGetDataTableAsync(ShowFeaturesViewModel features)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _featureService.GetCategoryFeatures(features));
    }

    public async Task<IActionResult> OnPostDelete(long categoryId, long featureId)
    {
        var categoryFeature = await _categoryFeatureService.GetCategoryFeature(categoryId, featureId);
        if (categoryFeature is not null)
        {
            _categoryFeatureService.Remove(categoryFeature);
            await _uow.SaveChangesAsync();
        }

        return Json(new JsonResultOperation(true, "ویژگی دسته بندی مورد نظر با موفقیت حذف شد"));
    }

    public async Task<IActionResult> OnGetAdd()
    {
        var categories = await _categoryService.GetCategoriesToShowInSelectBoxAsync();
        var model = new AddFeatureViewModel()
        {
            Categories = categories.CreateSelectListItem()
        };
        return Partial("Add", model);
    }

    public async Task<IActionResult> OnGetAutocompleteSearch(string term)
    {
        return Json(await _featureService.AutocompleteSearch(term));
    }
}