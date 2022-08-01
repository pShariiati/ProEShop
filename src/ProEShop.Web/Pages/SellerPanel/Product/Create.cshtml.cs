using System.Text;
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
    private readonly ICategoryBrandService _categoryBrandService;
    private readonly IProductShortLinkService _productShortLinkService;

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
        IProductService productService,
        ICategoryBrandService categoryBrandService, IProductShortLinkService productShortLinkService)
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
        _categoryBrandService = categoryBrandService;
        _productShortLinkService = productShortLinkService;
    }

    #endregion
    
    public AddProductViewModel Product { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(AddProductViewModel product)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }

        var categoriesToAdd = await _categoryService.GetCategoryParentIds(Product.MainCategoryId);
        if (!categoriesToAdd.isSuccessful)
        {
            return Json(new JsonResultOperation(false));
        }

        if (!await _categoryBrandService.CheckCategoryBrand(Product.MainCategoryId, Product.BrandId))
        {
            return Json(new JsonResultOperation(false));
        }
        var productToAdd = _mapper.Map<Entities.Product>(Product);

        var shortLint = await _productShortLinkService.GetProductShortLinkForCreateProduct();
        productToAdd.ProductShortLinkId = shortLint.Id;
        shortLint.IsUsed = true;

        productToAdd.Slug = productToAdd.PersianTitle.ToUrlSlug();
        productToAdd.SellerId = await _sellerService.GetSellerId(User.Identity.GetLoggedInUserId());
        productToAdd.ProductCode = await _productService.GetProductCodeForCreateProduct();

        productToAdd.ShortDescription = _htmlSanitizer.Sanitize(Product.ShortDescription);
        productToAdd.SpecialtyCheck = _htmlSanitizer.Sanitize(Product.SpecialtyCheck);

        if (!await _categoryService.CanAddFakeProduct(Product.MainCategoryId))
        {
            productToAdd.IsFake = false;
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

        #region NonConstantValue

        var featureIds = new List<long>();

        var productFeatureValueInputs = Request.Form
            .Where(x => x.Key.StartsWith("ProductFeatureValue")).ToList();

        foreach (var item in productFeatureValueInputs)
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

        if (await _featureConstantValueService.CheckNonConstantValue(Product.MainCategoryId, featureIds))
        {
            return Json(new JsonResultOperation(false));
        }

        foreach (var item in productFeatureValueInputs)
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
                    }
                }
            }
            else
            {
                return Json(new JsonResultOperation(false));
            }
        }

        #endregion

        #region ConstantValue

        var featureConstantValueIds = new List<long>();

        var productFeatureConstantValueInputs = Request.Form
            .Where(x => x.Key.StartsWith("ProductFeatureConstantValue")).ToList();

        foreach (var item in productFeatureConstantValueInputs)
        {
            if (long.TryParse(item.Key.Replace("ProductFeatureConstantValue", string.Empty), out var featureId))
            {
                featureConstantValueIds.Add(featureId);
            }
            else
            {
                return Json(new JsonResultOperation(false));
            }
        }

        featureIds = featureIds.Concat(featureConstantValueIds).ToList();

        if (!await _categoryFeatureService.CheckCategoryFeaturesCount(Product.MainCategoryId, featureIds))
        {
            return Json(new JsonResultOperation(false));
        }

        if (!await _featureConstantValueService.CheckConstantValue(Product.MainCategoryId, featureConstantValueIds))
        {
            return Json(new JsonResultOperation(false));
        }

        var featureConstantValues =
            await _featureConstantValueService.GetFeatureConstantValuesForCreateProduct(Product.MainCategoryId);

        foreach (var item in productFeatureConstantValueInputs)
        {
            if (long.TryParse(item.Key.Replace("ProductFeatureConstantValue", string.Empty), out var featureId))
            {
                if (item.Value.Count > 0)
                {
                    var valueToAdd = new StringBuilder();
                    foreach (var value in item.Value)
                    {
                        var trimmedValue = value.Trim();
                        if (featureConstantValues.Where(x => x.FeatureId == featureId).Any(x => x.Value == trimmedValue))
                        {
                            valueToAdd.Append(trimmedValue + "|||");
                        }
                    }
                    if (productToAdd.ProductFeatures.All(x => x.FeatureId != featureId))
                    {
                        if (valueToAdd.ToString().Length > 0)
                        {
                            var a = valueToAdd.ToString()[..(valueToAdd.Length - 3)];
                            var b = valueToAdd.ToString().Substring(0, valueToAdd.Length - 3);
                            productToAdd.ProductFeatures.Add(new()
                            {
                                FeatureId = featureId,
                                Value = valueToAdd.ToString()[..(valueToAdd.Length - 3)]
                            });
                        }
                    }
                }
            }
            else
            {
                return Json(new JsonResultOperation(false));
            }
        }

        #endregion

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
                    "images", "products");
            }
        }

        var productVideos = productToAdd.ProductMedia
            .Where(x => x.IsVideo)
            .ToList();
        for (int counter = 0; counter < productVideos.Count; counter++)
        {
            var currentVideo = Product.Videos[counter];
            if (currentVideo.IsFileUploaded())
            {
                await _uploadFile.SaveFile(currentVideo, productVideos[counter].FileName, null,
                    "videos", "products");
            }
        }

        return Json(new JsonResultOperation(true, "محصول مورد نظر با موفقیت ایجاد شد")
        {
            Data = Url.Page(nameof(Successful))
        });
    }

    public void Successful()
    {

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

    public async Task<IActionResult> OnGetGetCommissionPercentage(long brandId, long categoryId)
    {
        if (brandId < 1 || categoryId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var result = await _categoryBrandService.GetCommissionPercentage(categoryId, brandId);
        if (!result.isSucessfull)
        {
            return Json(new JsonResultOperation(false));
        }

        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = result.value
        });
    }
}