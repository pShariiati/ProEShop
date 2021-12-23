using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Web.Pages.Admin.Category;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly ICategoryService _categoryService;
    private readonly IUnitOfWork _uow;
    private readonly IUploadFileService _uploadFile;

    public IndexModel(ICategoryService categoryService, IUnitOfWork uow, IUploadFileService uploadFile)
    {
        _categoryService = categoryService;
        _uow = uow;
        _uploadFile = uploadFile;
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
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        categories.Pagination.Take = 1;
        return Partial("List", await _categoryService.GetCategories(categories));
    }

    public IActionResult OnGetAdd()
    {
        var model = new AddCategoryViewModel()
        {
            MainCategories = _categoryService.GetCategoriesToShowInSelectBox()
                .CreateSelectListItem(firstItemText: "خودش دسته اصلی باشد")
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

        var category = new Entities.Category
        {
            Description = model.Description,
            ShowInMenus = model.ShowInMenus,
            Title = model.Title,
            Slug = model.Slug,
            ParentId = model.ParentId == 0 ? null : model.ParentId,
            Picture = pictureFileName
        };

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
        model.MainCategories = _categoryService.GetCategoriesToShowInSelectBox()
            .CreateSelectListItem(firstItemText: "خودش دسته اصلی باشد");
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

        string pictureFileName = null;
        if (model.Picture.IsFileUploaded())
            pictureFileName = model.Picture.GenerateFileName();

        var category = await _categoryService.FindByIdAsync(model.Id);
        if (category == null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        var oldFileName = category.Picture;

        category.Title = model.Title;
        category.Description = model.Description;
        category.ShowInMenus = model.ShowInMenus;
        category.Slug = model.Slug;
        category.ParentId = model.ParentId == 0 ? null : model.ParentId;
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
}