﻿using AutoMapper;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Admin.Brand;

public class IndexModel : PageBase
{
    private readonly IBrandService _brandService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IUploadFileService _uploadFile;

    public IndexModel(
        IBrandService brandService,
        IMapper mapper,
        IUnitOfWork uow,
        IUploadFileService uploadFile)
    {
        _brandService = brandService;
        _mapper = mapper;
        _uow = uow;
        _uploadFile = uploadFile;
    }

    #region Constructor
    
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
        brand.LogoPicture = model.LogoPicture.GenerateFileName();
        string brandRegistrationFileName = null;
        if (model.BrandRegistrationPicture.IsFileUploaded())
            brandRegistrationFileName = model.BrandRegistrationPicture.GenerateFileName();
        brand.BrandRegistrationPicture = brandRegistrationFileName;

        await _brandService.AddAsync(brand);
        await _uow.SaveChangesAsync();
        await _uploadFile.SaveFile(model.LogoPicture, brand.LogoPicture, null, "images", "brands");
        await _uploadFile.SaveFile(model.BrandRegistrationPicture, brandRegistrationFileName, null, "images", "brandregistrationpictures");
        return Json(new JsonResultOperation(true, "برند مورد نظر با موفقیت اضافه شد"));
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
        var oldLogoPictureFileName = brandToUpdate.LogoPicture;
        var oldBrandRegistrationFileName = brandToUpdate.BrandRegistrationPicture;

        brandToUpdate = _mapper.Map(model, brandToUpdate);

        string logoPictureFileName = null;
        if (model.NewLogoPicture.IsFileUploaded())
            logoPictureFileName = model.NewLogoPicture.GenerateFileName();
        brandToUpdate.LogoPicture = logoPictureFileName;

        string brandRegistrationFileName = null;
        if (model.NewBrandRegistrationPicture.IsFileUploaded())
            brandRegistrationFileName = model.NewBrandRegistrationPicture.GenerateFileName();
        brandToUpdate.BrandRegistrationPicture = brandRegistrationFileName;

        await _brandService.Update(brandToUpdate);
        await _uow.SaveChangesAsync();
        await _uploadFile.SaveFile(model.NewLogoPicture, brandToUpdate.LogoPicture, oldLogoPictureFileName, "images", "brands");
        await _uploadFile.SaveFile(model.NewBrandRegistrationPicture, brandToUpdate.BrandRegistrationPicture, oldBrandRegistrationFileName, "images", "brandregistrationpictures");
        return Json(new JsonResultOperation(true, "برند مورد نظر با موفقیت ویرایش شد"));
    }
}