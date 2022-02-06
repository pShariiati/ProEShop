using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.Services.Contracts.Identity;
using ProEShop.Services.Services.Identity;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Seller;

public class CreateSellerModel : PageBase
{
    #region Constructor

    private readonly IApplicationUserManager _userManager;
    private readonly IProvinceAndCityService _provinceAndCityService;
    private readonly ISellerService _sellerService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IUploadFileService _uploadFile;

    public CreateSellerModel(
        IApplicationUserManager userManager,
        IProvinceAndCityService provinceAndCityService, ISellerService sellerService, IMapper mapper, IUnitOfWork uow, IUploadFileService uploadFile)
    {
        _userManager = userManager;
        _provinceAndCityService = provinceAndCityService;
        _sellerService = sellerService;
        _mapper = mapper;
        _uow = uow;
        _uploadFile = uploadFile;
    }

    #endregion

    [BindProperty]
    [PageRemote(PageName = "CreateSeller", PageHandler = "CheckForShopName",
        HttpMethod = "POST",
        AdditionalFields = ViewModelConstants.AntiForgeryToken,
        ErrorMessage = AttributesErrorMessages.RemoteMessage)]
    [Display(Name = "نام فروشگاه")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(200, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string ShopName { get; set; }

    [BindProperty]
    public CreateSellerViewModel CreateSeller { get; set; }
        = new();

    public async Task<IActionResult> OnGet(string phoneNumber)
    {
        if (!await _userManager.CheckForUserIsSeller(phoneNumber))
        {
            return RedirectToPage("/Error");
        }
        CreateSeller.PhoneNumber = phoneNumber;
        CreateSeller = await _userManager.GetUserInfoForCreateSeller(phoneNumber);
        var provinces = await _provinceAndCityService.GetProvincesToShowInSelectBoxAsync();
        CreateSeller.Provinces = provinces.CreateSelectListItem();
        return Page();
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

        if (!CreateSeller.IsLegalPerson)
        {
            CreateSeller.CompanyName
                = CreateSeller.RegisterNumber
                    = CreateSeller.EconomicCode
                        = CreateSeller.SignatureOwners
                            = CreateSeller.NationalId
                                = null;
            CreateSeller.CompanyType = null;
        }
        else
        {
            var legalPersonProperties = new List<string>
            {
                nameof(CreateSeller.CompanyName),
                nameof(CreateSeller.RegisterNumber),
                nameof(CreateSeller.EconomicCode),
                nameof(CreateSeller.SignatureOwners),
                nameof(CreateSeller.NationalId)
            };

            ModelState.CheckStringInputs(legalPersonProperties, CreateSeller);
            if (!ModelState.IsValid)
            {
                return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
                {
                    Data = ModelState.GetModelStateErrors()
                });
            }
        }

        var user = await _userManager.GetUserForCreateSeller(CreateSeller.PhoneNumber);
        if (user is null)
        {
            return Json(new JsonResultOperation(false, "کاربر مورد نظر یافت نشد"));
        }
        user = _mapper.Map(CreateSeller, user);
        var birthDateResult = CreateSeller.BirthDate.ToGregorianDateForCreateSeller();
        if (!birthDateResult.IsOk)
        {
            return Json(new JsonResultOperation(false, "تاریخ تولد را به درستی وارد نمایید"));
        }

        if (!birthDateResult.IsGreaterThan18)
        {
            return Json(new JsonResultOperation(false, "سن شما باید بیشتر از ۱۸ سال باشد"));
        }

        user.BirthDate = birthDateResult.ConvertedDateTime;
        var seller = _mapper.Map<Entities.Seller>(CreateSeller);
        seller.UserId = user.Id;
        seller.ShopName = ShopName;
        

        string logoFileName = null;
        if (CreateSeller.LogoFile.IsFileUploaded())
            logoFileName = CreateSeller.LogoFile.GenerateFileName();

        seller.IdCartPicture = CreateSeller.IdCartPictureFile.GenerateFileName();
        seller.Logo = logoFileName;

        seller.SellerCode = await _sellerService.GetSellerCodeForCreateSeller();

        var result = await _sellerService.AddAsync(seller);
        if (!result.Ok)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.DuplicateErrorMessage)
            {
                Data = result.Columns.SetDuplicateColumnsErrorMessages<CreateSellerViewModel>()
            });
        }

        var roleResult = await _userManager.AddToRoleAsync(user, ConstantRoles.Seller);
        if (!roleResult.Succeeded)
        {
            ModelState.AddErrorsFromResult(roleResult);
            return Json(new JsonResultOperation(false)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        await _uow.SaveChangesAsync();

        await _uploadFile.SaveFile(CreateSeller.IdCartPictureFile, seller.IdCartPicture, null, "images", "seller-id-cart-pictures");
        if (logoFileName != null)
            await _uploadFile.SaveFile(CreateSeller.LogoFile, logoFileName, null, "images", "seller-logos");

        return Json(new JsonResultOperation(true, "شما با موفقیت به عنوان فروشنده انتخاب شدید")
        {
            Data = "/Seller/RegistrationDone"
        });
    }

    public async Task<IActionResult> OnGetGetCities(long provinceId)
    {
        if (provinceId == 0)
        {
            return Json(new JsonResultOperation(true, string.Empty)
            {
                Data = new Dictionary<long, string>()
            });
        }

        if (provinceId < 1)
        {
            return Json(new JsonResultOperation(false, "استان مورد نظر را به درستی وارد نمایید"));
        }

        if (!await _provinceAndCityService.IsExistsBy(nameof(Entities.ProvinceAndCity.Id), provinceId))
        {
            return Json(new JsonResultOperation(false, "استان مورد نظر یافت نشد"));
        }

        var cities = await _provinceAndCityService.GetCitiesByProvinceIdInSelectBoxAsync(provinceId);
        return Json(new JsonResultOperation(true, string.Empty)
        {
            Data = cities
        });
    }

    public async Task<IActionResult> OnPostCheckForShopName(string shopName)
    {
        return Json(!await _sellerService.IsExistsBy(nameof(Entities.Seller.ShopName),
            shopName));
    }
    public async Task<IActionResult> OnGetCheckForShabaNumber(CreateSellerViewModel createSeller)
    {
        return Json(!await _sellerService.IsExistsBy(nameof(Entities.Seller.ShabaNumber),
            createSeller.ShabaNumber));
    }
}