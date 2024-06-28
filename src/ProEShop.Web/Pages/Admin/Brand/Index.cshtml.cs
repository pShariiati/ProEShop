using AutoMapper;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Brands;

namespace ProEShop.Web.Pages.Admin.Brand;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly IBrandService _brandService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IUploadFileService _uploadFile;
    private readonly IHtmlSanitizer _htmlSanitizer;

    public IndexModel(
        IBrandService brandService,
        IMapper mapper,
        IUnitOfWork uow,
        IUploadFileService uploadFile,
        IHtmlSanitizer htmlSanitizer)
    {
        _brandService = brandService;
        _mapper = mapper;
        _uow = uow;
        _uploadFile = uploadFile;
        _htmlSanitizer = htmlSanitizer;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowBrandsViewModel Brands { get; set; }
        = new();

    public void OnGet()
    {
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
        return Partial("List", await _brandService.GetBrands(Brands));
    }

    public IActionResult OnGetAdd()
    {
        return Partial("Add");
    }

    public async Task<IActionResult> OnPostAddAsync(AddBrandViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        var brand = _mapper.Map<Entities.Brand>(model);
        brand.Slug = brand.TitleEn.ToUrlSlug();
        brand.Description = _htmlSanitizer.Sanitize(brand.Description);
        brand.IsConfirmed = true;

        string brandLogoFileName = null;
        if (model.LogoPicture.IsFileUploaded())
            brandLogoFileName = model.LogoPicture.GenerateFileName();
        brand.LogoPicture = brandLogoFileName;

        string brandRegistrationFileName = null;
        if (model.BrandRegistrationPicture.IsFileUploaded())
            brandRegistrationFileName = model.BrandRegistrationPicture.GenerateFileName();
        brand.BrandRegistrationPicture = brandRegistrationFileName;

        var result = await _brandService.AddAsync(brand);
        if (!result.Ok)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.DuplicateErrorMessage)
            {
                Data = result.Columns.SetDuplicateColumnsErrorMessages<AddBrandViewModel>()
            });
        }
        await _uow.SaveChangesAsync();
        await _uploadFile.SaveFile(model.LogoPicture, brand.LogoPicture, null, "images", "brands");
        await _uploadFile.SaveFile(model.BrandRegistrationPicture, brandRegistrationFileName, null, "images", "brandregistrationpictures");
        return Json(new JsonResultOperation(true, "برند مورد نظر با موفقیت اضافه شد"));
    }

    public async Task<IActionResult> OnGetCheckForTitleFa(string titleFa)
    {
        return Json(!await _brandService.IsExistsBy(nameof(Entities.Brand.TitleFa), titleFa));
    }

    public async Task<IActionResult> OnGetCheckForTitleEn(string titleEn)
    {
        return Json(!await _brandService.IsExistsBy(nameof(Entities.Brand.TitleEn), titleEn));
    }

    public async Task<IActionResult> OnGetCheckForTitleFaOnEdit(string titleFa, long id)
    {
        return Json(!await _brandService.IsExistsBy(nameof(Entities.Brand.TitleFa), titleFa, id));
    }

    public async Task<IActionResult> OnGetCheckForTitleEnOnEdit(string titleEn, long id)
    {
        return Json(!await _brandService.IsExistsBy(nameof(Entities.Brand.TitleEn), titleEn, id));
    }

    public async Task<IActionResult> OnGetEdit(long id)
    {
        var model = await _brandService.GetForEdit(id);
        if (model is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }
        return Partial("Edit", model);
    }

    public async Task<IActionResult> OnPostEdit(EditBrandViewMode model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        var brandToUpdate = await _brandService.FindByIdAsync(model.Id);
        if (brandToUpdate is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        brandToUpdate.Slug = model.TitleEn.ToUrlSlug();
        var oldLogoPictureFileName = brandToUpdate.LogoPicture;
        var oldBrandRegistrationFileName = brandToUpdate.BrandRegistrationPicture;

        brandToUpdate = _mapper.Map(model, brandToUpdate);

        string logoPictureFileName = null;
        if (model.NewLogoPicture.IsFileUploaded())
            logoPictureFileName = model.NewLogoPicture.GenerateFileName();
        brandToUpdate.LogoPicture = logoPictureFileName;

        if (model.NewBrandRegistrationPicture.IsFileUploaded())
            brandToUpdate.BrandRegistrationPicture = model.NewBrandRegistrationPicture.GenerateFileName();
        else
            brandToUpdate.LogoPicture = oldLogoPictureFileName;

        var result = await _brandService.Update(brandToUpdate);
        if (!result.Ok)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.DuplicateErrorMessage)
            {
                Data = result.Columns.SetDuplicateColumnsErrorMessages<AddBrandViewModel>()
            });
        }
        await _uow.SaveChangesAsync();
        await _uploadFile.SaveFile(model.NewLogoPicture, brandToUpdate.LogoPicture, oldLogoPictureFileName, "images", "brands");
        await _uploadFile.SaveFile(model.NewBrandRegistrationPicture, brandToUpdate.BrandRegistrationPicture, oldBrandRegistrationFileName, "images", "brandregistrationpictures");
        return Json(new JsonResultOperation(true, "برند مورد نظر با موفقیت ویرایش شد"));
    }

    public async Task<IActionResult> OnGetBrandDetails(long brandId)
    {
        var model = await _brandService.GetBrandDetails(brandId);
        if (model is null)
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        return Partial("BrandDetails", model);
    }

    public async Task<IActionResult> OnPostRejectBrand(BrandDetailsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, "لطفا دلیل رد برند را وارد نمایید"));
        }

        var brand = await _brandService.GetInActiveBrand(model.Id);
        if (brand is null)
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        _brandService.Remove(brand);
        await _uow.SaveChangesAsync();
        //todo: send reject reasons to seller Email
        return Json(new JsonResultOperation(true, "برند مورد نظر با موفقیت حذف شد"));
    }

    public async Task<IActionResult> OnPostConfirmBrand(long id)
    {
        var brand = await _brandService.GetInActiveBrand(id);
        if (brand is null)
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        brand.IsConfirmed = true;
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "برند مورد نظر با موفقیت شد"));
    }
}