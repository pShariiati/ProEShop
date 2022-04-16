using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.SellerPanel.Product;

public class CreateModel : SellerPanelBase
{
    #region Constructor

    private readonly ICategoryService _categoryService;
    private readonly ICategoryFeatureService _categoryFeatureService;
    private readonly IBrandService _brandService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IUploadFileService _uploadFile;
    private readonly ISellerService _sellerService;

    public CreateModel(
        ICategoryService categoryService,
        IBrandService brandService,
        IMapper mapper,
        IUnitOfWork uow,
        IUploadFileService uploadFile,
        ISellerService sellerService,
        ICategoryFeatureService categoryFeatureService)
    {
        _categoryService = categoryService;
        _brandService = brandService;
        _mapper = mapper;
        _uow = uow;
        _uploadFile = uploadFile;
        _sellerService = sellerService;
        _categoryFeatureService = categoryFeatureService;
    }

    #endregion

    public AddProductViewModel Product { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetGetCategories(long[] selectedCategoriesIds)
    {
        var result = await _categoryService.GetCategoriesForCreateProduct(selectedCategoriesIds);
        return Partial("_SelectProductCategoryPartial", result);
    }

    public async Task<IActionResult> OnGetGetCategoryBrands(long categoryId)
    {
        if (categoryId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var brands = await _brandService.GetBrandsByCategoryId(categoryId);
        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = brands
        });
    }

    public async Task<IActionResult> OnGetCanAddFakeProduct(long categoryId)
    {
        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = await _categoryService.CanAddFakeProduct(categoryId)
        });
    }

    public IActionResult OnGetRequestForAddBrand(long categoryId)
    {
        return Partial("_RequestForAddBrandPartial");
    }

    public async Task<IActionResult> OnPostRequestForAddBrand(AddBrandBySellerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        var brand = _mapper.Map<Entities.Brand>(model);
        
        // Add brand category
        brand.CategoryBrands.Add(new CategoryBrand()
        {
            CategoryId = model.CategoryId
        });

        var userId = User.Identity.GetUserId();
        brand.SellerId = await _sellerService.GetSellerId(userId.Value);
        brand.LogoPicture = model.LogoPicture.GenerateFileName();
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
        return Json(new JsonResultOperation(true, "برند ثبت شد و پس از تایید کارشناسان قابل دسترسی است، مراتب از طریق ایمیل به شما اطلاع رسانی خواهد شد"));
    }

    public async Task<IActionResult> OnGetCheckForTitleFa(string titleFa)
    {
        return Json(!await _brandService.IsExistsBy(nameof(Entities.Brand.TitleFa), titleFa));
    }

    public async Task<IActionResult> OnGetCheckForTitleEn(string titleEn)
    {
        return Json(!await _brandService.IsExistsBy(nameof(Entities.Brand.TitleEn), titleEn));
    }

    public IActionResult OnPostUploadSpecialtyCheckImages([IsImage] IFormFile file)
    {
        if (ModelState.IsValid && file.IsFileUploaded())
        {
            var imageFileName = file.GenerateFileName();
            _uploadFile.SaveFile(file, imageFileName, null, "images", "products", "specialty-check-images");
            return Json(new
            {
                location = $"/images/products/specialty-check-images/{imageFileName}"
            });
        }
        return Json(false);
    }

    public IActionResult OnPostUploadShortDescriptionImages([IsImage] IFormFile file)
    {
        if (ModelState.IsValid && file.IsFileUploaded())
        {
            var imageFileName = file.GenerateFileName();
            _uploadFile.SaveFile(file, imageFileName, null, "images", "products", "short-description-images");
            return Json(new
            {
                location = $"/images/products/short-description-images/{imageFileName}"
            });
        }
        return Json(false);
    }

    public async Task<IActionResult> OnGetShowCategoryFeatures(long categoryId)
    {
        return Partial("_ShowCategoryFeaturesPartial", await _categoryFeatureService.GetCategoryFeatures(categoryId));
    }
}