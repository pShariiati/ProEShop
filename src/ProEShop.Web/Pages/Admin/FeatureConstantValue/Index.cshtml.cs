using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.FeatureConstantValues;

namespace ProEShop.Web.Pages.Admin.FeatureConstantValue;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IFeatureConstantValueService _featureConstantValueService;
    private readonly ICategoryService _categoryService;
    private readonly ICategoryFeatureService _categoryFeatureService;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public IndexModel(
        IFeatureConstantValueService featureConstantValueService,
        ICategoryService categoryService,
        ICategoryFeatureService categoryFeatureService,IUnitOfWork uow,
        IMapper mapper)
    {
        _featureConstantValueService = featureConstantValueService;
        _categoryService = categoryService;
        _categoryFeatureService = categoryFeatureService;
        _uow = uow;
        _mapper = mapper;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowFeatureConstantValuesViewModel FeatureConstantValues { get; set; }
        = new();

    public async Task OnGet()
    {
        var categories = await _categoryService.GetCategoriesToShowInSelectBoxAsync();
        FeatureConstantValues.SearchFeatureConstantValues.Categories = categories.CreateSelectListItem();
    }

    public async Task<IActionResult> OnGetGetDataTableAsync()
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, PublicConstantStrings.ModelStateErrorMessage);
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _featureConstantValueService.GetFeatureConstantValues(FeatureConstantValues));
    }

    public async Task<IActionResult> OnGetGetCategoryFeatures(long categoryId)
    {
        if (categoryId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = await _categoryFeatureService.GetCategoryFeatures(categoryId)
        });
    }

    public async Task<IActionResult> OnPostDelete(long featureConstantValueId)
    {
        if (featureConstantValueId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var featureConstantValue = await _featureConstantValueService.FindByIdAsync(featureConstantValueId);
        if (featureConstantValue is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        _featureConstantValueService.Remove(featureConstantValue);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "مقدار ثابت ویژگی با موفقیت حذف شد"));
    }

    public async Task<IActionResult> OnGetAdd()
    {
        var categories = await _categoryService.GetCategoriesToShowInSelectBoxAsync();
        var model = new AddFeatureConstantValueViewModel()
        {
            Categories = categories.CreateSelectListItem()
    };
        return Partial("Add", model);
    }

    public async Task<IActionResult> OnPostAdd(AddFeatureConstantValueViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        if (!await _categoryFeatureService.CheckCategoryFeature(model.CategoryId, model.FeatureId))
        {
            return Json(new JsonResultOperation(false));
        }
        var featureConstantValueToAdd = _mapper.Map<Entities.FeatureConstantValue>(model);
        await _featureConstantValueService.AddAsync(featureConstantValueToAdd);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "مقدار ثابت ویژگی با موفقیت اضافه شد"));
    }
}