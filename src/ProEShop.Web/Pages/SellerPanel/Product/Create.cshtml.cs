using AutoMapper;
using Ganss.XSS;
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
using ProEShop.ViewModels.CategoryFeatures;
using ProEShop.ViewModels.FeatureConstantValues;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Pages.SellerPanel.Product;

public class CreateModel : SellerPanelBase
{
    #region Constructor

    private readonly ICategoryService _categoryService;
    private readonly ICategoryFeatureService _categoryFeatureService;
    private readonly IFeatureConstantValueService _featureConstantValueService;
    private readonly IBrandService _brandService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IUploadFileService _uploadFile;
    private readonly ISellerService _sellerService;
    private readonly IViewRendererService _viewRendererService;
    private readonly IHtmlSanitizer _htmlSanitizer;
    private readonly IProductService _productService;

    public CreateModel(
        ICategoryService categoryService,
        IBrandService brandService,
        IMapper mapper,
        IUnitOfWork uow,
        IUploadFileService uploadFile,
        ISellerService sellerService,
        ICategoryFeatureService categoryFeatureService,
        IFeatureConstantValueService featureConstantValueService,
        IViewRendererService viewRendererService,
        IHtmlSanitizer htmlSanitizer,
        IProductService productService)
    {
        _categoryService = categoryService;
        _brandService = brandService;
        _mapper = mapper;
        _uow = uow;
        _uploadFile = uploadFile;
        _sellerService = sellerService;
        _categoryFeatureService = categoryFeatureService;
        _featureConstantValueService = featureConstantValueService;
        _viewRendererService = viewRendererService;
        _htmlSanitizer = htmlSanitizer;
        _productService = productService;
    }

    #endregion

    [BindProperty]
    public AddProductViewModel Product { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        var productToAdd = _mapper.Map<Entities.Product>(Product);
        productToAdd.ShortDescription = _htmlSanitizer.Sanitize(Product.ShortDescription);
        productToAdd.SpecialtyCheck = _htmlSanitizer.Sanitize(Product.SpecialtyCheck);

        var categoriesToAdd = await _categoryService.GetCategoryParentIds(Product.CategoryId);
        if (!categoriesToAdd.isSuccessful)
        {
            return Json(new JsonResultOperation(false));
        }

        foreach (var categoryId in categoriesToAdd.categoryIds)
        {
            productToAdd.ProductCategories.Add(new ProductCategory()
            {
                CategoryId = categoryId
            });
        }

        foreach (var picture in Product.Pictures)
        {
            if (picture.IsFileUploaded())
            {
                var fileName = picture.GenerateFileName();
                productToAdd.ProductMedia.Add(new ProductMedia()
                {
                    FileName = fileName,
                    IsVideo = false
                });
            }
        }

        foreach (var video in Product.Videos)
        {
            if (video.IsFileUploaded())
            {
                var fileName = video.GenerateFileName();
                productToAdd.ProductMedia.Add(new ProductMedia()
                {
                    FileName = fileName,
                    IsVideo = true
                });
            }
        }

        var featureIds = new List<long>();

        foreach (var item in Request.Form
                     .Where(x => x.Key.StartsWith("ProductFeatureValue")).ToList())
        {
            if (long.TryParse(item.Key.Replace("ProductFeatureValue", string.Empty), out var featureId))
            {
                featureIds.Add(featureId);
            }
            else
            {
                return Json(new JsonResultOperation(false));
            }
        }

        if (!await _categoryFeatureService.CheckCategoryFeaturesCount(Product.CategoryId, featureIds))
        {
            return Json(new JsonResultOperation(false));
        }

        foreach (var item in Request.Form
                     .Where(x => x.Key.StartsWith("ProductFeatureValue")).ToList())
        {
            if (long.TryParse(item.Key.Replace("ProductFeatureValue", string.Empty), out var featureId))
            {
                var trimmedValue = item.Value.ToString().Trim();
                if (productToAdd.ProductFeatures.All(x => x.FeatureId != featureId))
                {
                    if (trimmedValue.Length > 0)
                    {
                        productToAdd.ProductFeatures.Add(new ProductFeature()
                        {
                            FeatureId = featureId,
                            Value = trimmedValue
                        });
                        featureIds.Add(featureId);
                    }
                }
            }
            else
            {
                return Json(new JsonResultOperation(false));
            }
        }

        await _productService.AddAsync(productToAdd);
        await _uow.SaveChangesAsync();

        var productPictures = productToAdd.ProductMedia
            .Where(x => !x.IsVideo)
            .ToList();
        for (int counter = 0; counter < productPictures.Count; counter++)
        {
            var currentPicture = Product.Pictures[counter];
            if (currentPicture.IsFileUploaded())
            {
                await _uploadFile.SaveFile(currentPicture, productPictures[counter].FileName, null,
                    "images", "products", "images");
            }
        }

        var productVideos = productToAdd.ProductMedia
            .Where(x => x.IsVideo)
            .ToList();
        for (int counter = 0; counter < productVideos.Count; counter++)
        {
            var currentVideo = Product.Pictures[counter];
            if (currentVideo.IsFileUploaded())
            {
                await _uploadFile.SaveFile(currentVideo, productVideos[counter].FileName, null,
                    "images", "products", "videos");
            }
        }

        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = string.Empty
        });
    }

    public async Task<IActionResult> OnGetGetCategories(long[] selectedCategoriesIds)
    {
        var result = await _categoryService.GetCategoriesForCreateProduct(selectedCategoriesIds);
        return Partial("_SelectProductCategoryPartial", result);
    }

    public async Task<IActionResult> OnGetGetCategoryInfo(long categoryId)
    {
        if (categoryId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var categoryFeatureModel = new ProductFeaturesForCreateProductViewModel
        {
            Features = await _categoryFeatureService.GetCategoryFeatures(categoryId),
            FeaturesConstantValues = await _featureConstantValueService.GetFeatureConstantValuesByCategoryId(categoryId)
        };

        var model = new
        {
            Brands = await _brandService.GetBrandsByCategoryId(categoryId),
            CanAddFakeProduct = await _categoryService.CanAddFakeProduct(categoryId),
            CategoryFeatures = await _viewRendererService.RenderViewToStringAsync(
            "~/Pages/SellerPanel/Product/_ShowCategoryFeaturesPartial.cshtml", categoryFeatureModel)
        };

        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = model
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
}