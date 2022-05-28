using System.Linq.Expressions;
using AutoMapper;
using Ganss.XSS;
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

namespace ProEShop.Web.Pages.Admin.Category;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly ICategoryService _categoryService;
    private readonly IUnitOfWork _uow;
    private readonly IUploadFileService _uploadFile;
    private readonly IBrandService _brandService;
    private readonly IMapper _mapper;
    private readonly IHtmlSanitizer _htmlSanitizer;

    public IndexModel(
        ICategoryService categoryService,
        IUnitOfWork uow,
        IUploadFileService uploadFile,
        IBrandService brandService,
        IMapper mapper,
        IHtmlSanitizer htmlSanitizer)
    {
        _categoryService = categoryService;
        _uow = uow;
        _uploadFile = uploadFile;
        _brandService = brandService;
        _mapper = mapper;
        _htmlSanitizer = htmlSanitizer;
    }

    #endregion

    public ShowCategoriesViewModel Categories { get; set; }
    = new();

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnGetGetDataTableAsync(ShowCategoriesViewModel categories)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, PublicConstantStrings.ModelStateErrorMessage);
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _categoryService.GetCategories(categories));
    }

    public async Task<IActionResult> OnGetAdd(long id = 0)
    {
        if (id > 0)
        {
            if (!await _categoryService.IsExistsBy(nameof(Entities.Category.Id), id))
            {
                return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
            }
        }

        var categories = await _categoryService.GetCategoriesToShowInSelectBoxAsync();
        var model = new AddCategoryViewModel()
        {
            ParentId = id,
            MainCategories = categories.CreateSelectListItem(firstItemText: "خودش دسته اصلی باشد")
        };
        return Partial("Add", model);
    }

    public async Task<IActionResult> OnPostAdd(AddCategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        string pictureFileName = null;
        if (model.Picture.IsFileUploaded())
            pictureFileName = model.Picture.GenerateFileName();

        var category = _mapper.Map<Entities.Category>(model);
        category.Description = _htmlSanitizer.Sanitize(category.Description);
        if (model.ParentId is 0)
            category.ParentId = null;
        category.Picture = pictureFileName;

        var result = await _categoryService.AddAsync(category);
        if (!result.Ok)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.DuplicateErrorMessage)
            {
                Data = result.Columns.SetDuplicateColumnsErrorMessages<AddCategoryViewModel>()
            });
        }
        await _uow.SaveChangesAsync();
        await _uploadFile.SaveFile(model.Picture, pictureFileName, null, "images", "categories");
        return Json(new JsonResultOperation(true, "دسته بندی مورد نظر با موفقیت اضافه شد"));
    }

    public async Task<IActionResult> OnGetEdit(long id)
    {
        var model = await _categoryService.GetForEdit(id);
        if (model is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        var categories = await _categoryService.GetCategoriesToShowInSelectBoxAsync(id);
        model.MainCategories = categories.CreateSelectListItem(firstItemText: "خودش دسته اصلی باشد");
        return Partial("Edit", model);
    }

    public async Task<IActionResult> OnPostEdit(EditCategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        if (model.Id == model.ParentId)
        {
            return Json(new JsonResultOperation(false, "یک رکورد نمیتواند والد خودش باشد"));
        }

        string pictureFileName = null;
        if (model.Picture.IsFileUploaded())
            pictureFileName = model.Picture.GenerateFileName();

        var category = await _categoryService.FindByIdAsync(model.Id);
        if (category == null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        var oldFileName = category.Picture;

        category = _mapper.Map(model, category);
        if (model.ParentId is 0)
            category.ParentId = null;
        category.Picture = pictureFileName;

        var result = await _categoryService.Update(category);
        if (!result.Ok)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.DuplicateErrorMessage)
            {
                Data = result.Columns.SetDuplicateColumnsErrorMessages<AddCategoryViewModel>()
            });
        }
        await _uow.SaveChangesAsync();
        await _uploadFile.SaveFile(model.Picture, pictureFileName, oldFileName, "images", "categories");
        return Json(new JsonResultOperation(true, "دسته بندی مورد نظر با موفقیت ویرایش شد"));
    }

    public async Task<IActionResult> OnPostDeleteAsync(long elementId)
    {
        var category = await _categoryService.FindByIdAsync(elementId);
        if (category is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }
        _categoryService.SoftDelete(category);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "دسته بندی مورد نظر با موفقیت حذف شد"));
    }

    public async Task<IActionResult> OnPostDeletePicture(long elementId)
    {
        var category = await _categoryService.FindByIdAsync(elementId);
        if (category is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        var fileName = category.Picture;
        category.Picture = null;
        await _uow.SaveChangesAsync();
        _uploadFile.DeleteFile(fileName, "images", "categories");
        return Json(new JsonResultOperation(true, "تصویر دسته بندی مورد نظر با موفقیت حذف شد"));
    }

    public async Task<IActionResult> OnPostRestoreAsync(long elementId)
    {
        var category = await _categoryService.FindByIdAsync(elementId);
        if (category is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }
        _categoryService.Restore(category);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "دسته بندی مورد نظر با موفقیت بازگردانی شد"));
    }

    public async Task<IActionResult> OnPostCheckForTitle(string title)
    {
        return Json(!await _categoryService.IsExistsBy(nameof(Entities.Category.Title), title));
    }

    public async Task<IActionResult> OnPostCheckForSlug(string slug)
    {
        return Json(!await _categoryService.IsExistsBy(nameof(Entities.Category.Slug), slug));
    }

    public async Task<IActionResult> OnPostCheckForTitleOnEdit(string title, long id)
    {
        return Json(!await _categoryService.IsExistsBy(nameof(Entities.Category.Title), title, id));
    }

    public async Task<IActionResult> OnPostCheckForSlugOnEdit(string slug, long id)
    {
        return Json(!await _categoryService.IsExistsBy(nameof(Entities.Category.Slug), slug, id));
    }

    public async Task<IActionResult> OnGetAddBrand(long selectedCategoryId)
    {
        var model = new AddBrandToCategoryViewModel
        {
            SelectedBrands = await _categoryService.GetCategoryBrands(selectedCategoryId)
        };
        return Partial("AddBrand", model);
    }

    public async Task<IActionResult> OnPostAddBrand(AddBrandToCategoryViewModel model)
    {
        if (model.SelectedCategoryId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var selectedCategory = await _categoryService.GetCategoryWithItsBrands(model.SelectedCategoryId);
        if (selectedCategory is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }
        selectedCategory.CategoryBrands.Clear();

        model.SelectedBrands = model.SelectedBrands.Distinct().ToList();
        var brandsInDictionary = new Dictionary<string, byte>();
        foreach (var brand in model.SelectedBrands)
        {
            // سامسونگ ||| 1
            var splitBrand = brand.Split("|||");
            if (!byte.TryParse(splitBrand[1], out var commissionPercentage))
            {
                return Json(new JsonResultOperation(false));
            }

            if (commissionPercentage > 20 || commissionPercentage < 1)
            {
                return Json(new JsonResultOperation(false));
            }
            brandsInDictionary.Add(splitBrand[0], commissionPercentage);
        }

        var brands = await _brandService
            .GetBrandsByFullTitle(brandsInDictionary.Select(x=>x.Key).ToList());
        // اگر کاربر سه برند را سمت کلاینت وارد کرد
        // باید همان مقدار را از پایگاه داده بخوانیم
        // و اگر اینطور نبود حتما یک یا چند برند را وارد کرده
        // که در پایگاه داده ما وجود ندارد
        if (model.SelectedBrands.Count != brands.Count)
        {
            return Json(new JsonResultOperation(false));
        }

        foreach (var brand in brands)
        {
            var commissionPercentage = brandsInDictionary[brand.Value];
            selectedCategory.CategoryBrands.Add(new CategoryBrand()
            {
                BrandId = brand.Key,
                CommissionPercentage = commissionPercentage
            });
        }
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true,
            "برند های مورد نظر با موفقیت به دسته بندی مذکور اضافه شدند"));
    }

    public async Task<IActionResult> OnGetAutocompleteSearch(string term)
    {
        return Json(await _brandService.AutocompleteSearch(term));
    }
}