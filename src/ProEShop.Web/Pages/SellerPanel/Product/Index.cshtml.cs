using AutoMapper;
using DNTPersianUtils.Core;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.ProductVariants;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.SellerPanel.Product;

[CheckModelStateInRazorPages]
public class IndexModel : SellerPanelBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly ISellerService _sellerService;
    private readonly ICategoryService _categoryService;
    private readonly IUploadFileService _uploadFile;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IHtmlSanitizer _htmlSanitizer;
    private readonly IProductVariantService _productVariantService;

    public IndexModel(
        IProductService productService,
        ISellerService sellerService,
        ICategoryService categoryService,
        IUploadFileService uploadFile,
        IUnitOfWork uow,
        IHtmlSanitizer htmlSanitizer,
        IProductVariantService productVariantService,
        IMapper mapper)
    {
        _productService = productService;
        _sellerService = sellerService;
        _categoryService = categoryService;
        _uploadFile = uploadFile;
        _uow = uow;
        _htmlSanitizer = htmlSanitizer;
        _productVariantService = productVariantService;
        _mapper = mapper;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowProductsInSellerPanelViewModel Products { get; set; }
        = new();

    public void OnGet()
    {
        Products.SearchProducts.Categories = _categoryService.GetSellerCategories()
            .Result.CreateSelectListItem(firstItemText: "همه", firstItemValue: string.Empty);
    }

    public async Task<IActionResult> OnGetGetDataTableAsync()
    {
        return Partial("List", await _productService.GetProductsInSellerPanel(Products));
    }

    public async Task<IActionResult> OnGetGetProductDetails(long productId)
    {
        var product = await _productService.GetProductDetails(productId);
        if (product is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }
        return Partial("ProductDetails", product);
    }

    public async Task<IActionResult> OnGetAutocompleteSearchForPersianTitle(string term)
    {
        return Json(await _productService.GetPersianTitlesForAutocompleteInSellerPanel(term));
    }

    public async Task<IActionResult> OnGetShowProductVariantsAsync(long productId)
    {
        if (productId < 1)
        {
            return Json(new JsonResultOperation(false));
        }
        return Partial("ProductVariants", await _productVariantService.GetProductVariants(productId));
    }

    public async Task<IActionResult> OnGetEditProductVariant(long productVariantId)
    {
        if (productVariantId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var productVariant = await _productVariantService.GetDataForEdit(productVariantId);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        return Partial("_EditProductVariantPartial", productVariant);
    }

    public async Task<IActionResult> OnPostEditProductVariant(EditProductVariantViewModel model)
    {
        var productVariant = await _productVariantService.GetForEdit(model.Id);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        //_mapper.Map(model, productVariant);
        productVariant.Price = model.Price;
        productVariant.StartDateTime = productVariant.EndDateTime = null;
        productVariant.OffPrice = productVariant.OffPercentage = null;
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "تنوع محصول مورد نظر با موفقیت ویرایش شد"));
    }

    public async Task<IActionResult> OnGetAddEditDiscount(long productVariantId)
    {
        if (productVariantId < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var productVariant = await _productVariantService.GetDataForAddEditDiscount(productVariantId);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        return Partial("_AddEditDiscountPartial", productVariant);
    }

    public async Task<IActionResult> OnPostAddEditDiscount(AddEditDiscountViewModel model)
    {
        var productVariant = await _productVariantService.GetForEdit(model.Id);
        if (productVariant is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }

        var parsedDateTimes = DateTimeHelper.ConvertDateTimeForAddEditDiscount(model.StartDateTime, model.EndDateTime);
        if (!parsedDateTimes.IsSuccessful)
        {
            return Json(new JsonResultOperation(false, "لطفا تاریخ ها را به درستی وارد نمایید"));
        }

        if (parsedDateTimes.IsStartDateTimeGreatherOrEqualEndDateTime)
        {
            return Json(new JsonResultOperation(false, "تاریخ پایان تخفیف باید بزرگتر از تاریخ شروع تخفیف باشد"));
        }

        if (parsedDateTimes.IsTimeSpanLowerThan3Hour)
        {
            return Json(new JsonResultOperation(false, "تاریخ پایان تخفیف باید حداقل 3 ساعت بزرگتر از تاریخ شروع تخفیف باشد"));
        }

        var offPrice = model.OffPrice;
        var price = productVariant.Price;
        var offPercentage = model.OffPercentage;
        var discountPrice = price / 100 * offPercentage;
        var priceWithDiscount = price - discountPrice;
        var discountPriceSubtract1Percentage = price / 100 * (offPercentage - 1);
        var priceWithDiscountSubtract1Percentage = price - discountPriceSubtract1Percentage;

        // برای مثال قیمت کالا هزارتومان است
        // درصد تخفیف 7 درصد
        // یعنی میزان تخفیف 70 تومان است و مبلغ نهایی 930 تومان است
        // اگر قیمتی که در اینپوت تخفیف وارد میشود کمتر از 930 تومان باشد وارد
        // شاخه 8 درصد تخفیف میشود، پس باید به کاربر خطا نمایش دهیم
        // ما مخواهیم میزان تخفیف بین 6 تا 7 درصد باشد
        // بزرگتر از 6 و کوچکتر و مساوی 7 درصد
        // یعنی
        // OffPrice >= 930 && OffPrice < 940
        // شش درصد تخفیف روی هزارتومان میشود 940 تومان
        // اگر قرار است که مبلغ 940 تومان باشد پس باید شش درصد تخفیف وارد شود نه 7 درصد
        if (offPrice < priceWithDiscount || offPrice >= priceWithDiscountSubtract1Percentage)
        {
            return Json(new JsonResultOperation(false,
                $"قیمت تخفیف باید بزرگتر مساوی {priceWithDiscount.ToString("#,0").ToPersianNumbers()} تومان و کوچکتر از {priceWithDiscountSubtract1Percentage.ToString("#,0").ToPersianNumbers()} تومان باشد"));
        }

        productVariant.StartDateTime = parsedDateTimes.StartDate;
        productVariant.EndDateTime = parsedDateTimes.EndDate;
        _mapper.Map(model, productVariant);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "تنوع محصول مورد نظر با موفقیت ویرایش شد"));
    }
}