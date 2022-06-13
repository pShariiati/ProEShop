﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Variants;

namespace ProEShop.Web.Pages.SellerPanel.Product;

public class AddVariantModel : SellerPanelBase
{
    #region Constructor

    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly ISellerService _sellerService;
    private readonly IProductVariantService _productVariantService;
    private readonly IUnitOfWork _uow;

    public AddVariantModel(
        IProductService productService,
        IMapper mapper,
        ISellerService sellerService,
        IProductVariantService productVariantService,
        IUnitOfWork uow)
    {
        _productService = productService;
        _mapper = mapper;
        _sellerService = sellerService;
        _productVariantService = productVariantService;
        _uow = uow;
    }

    #endregion

    [BindProperty]
    public AddVariantViewModel Variant { get; set; }

    public async Task<IActionResult> OnGet(long productId)
    {
        var productInfo = await _productService.GetProductInfoForAddVariant(productId);
        if (productInfo is null)
        {
            return RedirectToPage(PublicConstantStrings.Error404PageName);
        }

        Variant = productInfo;
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

        var productVariantToAdd = _mapper.Map<Entities.ProductVariant>(Variant);

        // Get seller id for entity
        var userId = User.Identity.GetLoggedInUserId();
        var sellerId = await _sellerService.GetSellerId(userId);
        productVariantToAdd.SellerId = sellerId;

        await _productVariantService.AddAsync(productVariantToAdd);
        await _uow.SaveChangesAsync();

        return Json(new JsonResultOperation(true, "تنوع محصول با موفقیت اضافه شد")
        {
            Data = Url.Page("SuccessfulProductVariant")
        });
    }
}