using AutoMapper;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Admin.Seller;

public class IndexModel : PageBase
{
    #region Constructor

    private readonly ISellerService _sellerService;
    private readonly IHtmlSanitizer _htmlSanitizer;
    private readonly IUnitOfWork _uow;
    private readonly IUploadFileService _uploadFile;

    public IndexModel(
        ISellerService sellerService,
        IHtmlSanitizer htmlSanitizer,
        IUnitOfWork uow,
        IUploadFileService uploadFile)
    {
        _sellerService = sellerService;
        _htmlSanitizer = htmlSanitizer;
        _uow = uow;
        _uploadFile = uploadFile;
    }

    #endregion

    [BindProperty(SupportsGet = true)]
    public ShowSellersViewModel Sellers { get; set; }
        = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetGetDataTableAsync()
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = ModelState.GetModelStateErrors()
            });
        }
        return Partial("List", await _sellerService.GetSellers(Sellers));
    }

    public async Task<IActionResult> OnGetGetSellerDetails(long sellerId)
    {
        var seller = await _sellerService.GetSellerDetails(sellerId);
        if (seller is null)
        {
            return Json(new JsonResultOperation(false, PublicConstantStrings.RecordNotFoundMessage));
        }
        return Partial("SellerDetails", seller);
    }

    public async Task<IActionResult> OnPostRejectSellerDocuments(SellerDetailsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new JsonResultOperation(false, "لطفا دلایل رد مدراک فروشنده را وارد نمایید"));
        }

        var seller = await _sellerService.FindByIdAsync(model.Id);
        if (seller is null)
        {
            return Json(new JsonResultOperation(false, "فروشنده مورد نظر یافت نشد"));
        }

        seller.DocumentStatus = DocumentStatus.Rejected;
        seller.RejectReason = _htmlSanitizer.Sanitize(model.RejectReason);
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "مدارک فروشنده مورد نظر با موفقیت رد شد"));
    }

    public async Task<IActionResult> OnPostConfirmSellerDocuments(long id)
    {
        if (id < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var seller = await _sellerService.FindByIdAsync(id);
        if (seller is null)
        {
            return Json(new JsonResultOperation(false, "فروشنده مورد نظر یافت نشد"));
        }

        seller.DocumentStatus = DocumentStatus.Confirmed;
        seller.RejectReason = null;
        await _uow.SaveChangesAsync();
        return Json(new JsonResultOperation(true, "مدارک فروشنده مورد نظر با موفقیت تایید شد"));
    }

    public async Task<IActionResult> OnPostRemoveUser(long id)
    {
        if (id < 1)
        {
            return Json(new JsonResultOperation(false));
        }

        var seller = await _sellerService.GetSellerToRemoveInManagingSellers(id);
        if (seller is null)
        {
            return Json(new JsonResultOperation(false, "فروشنده مورد نظر یافت نشد"));
        }
        _sellerService.Remove(seller);
        await _uow.SaveChangesAsync();
        _uploadFile.DeleteFile(seller.IdCartPicture, "images", "seller-id-cart-pictures");
        _uploadFile.DeleteFile(seller.Logo, "images", "seller-logos");
        return Json(new JsonResultOperation(true, "فروشنده مورد نظر با موفقیت حذف شد"));
    }
}