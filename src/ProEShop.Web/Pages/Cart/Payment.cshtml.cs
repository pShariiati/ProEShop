using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Carts;

namespace ProEShop.Web.Pages.Cart;

[Authorize]
public class PaymentModel : PageBase
{
    #region Constructor

    private readonly ICartService _cartService;
    private readonly IAddressService _addressService;
    private readonly IOrderService _orderService;
    private readonly IUnitOfWork _uow;

    public PaymentModel(
        ICartService cartService,
        IAddressService addressService,
        IOrderService orderService,
        IUnitOfWork uow)
    {
        _cartService = cartService;
        _addressService = addressService;
        _orderService = orderService;
        _uow = uow;
    }

    #endregion

    public PaymentViewModel PaymentPage { get; set; }
        = new();

    public async Task<IActionResult> OnGet()
    {
        var userId = User.Identity.GetLoggedInUserId();
        PaymentPage.CartItems = await _cartService.GetCartsForPaymentPage(userId);

        // اگر سبد خرید خالی بود، کاربر رو به صفحه سبد خرید انتقال بده
        if (PaymentPage.CartItems.Count < 1)
        {
            return RedirectToPage("Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostCreateOrderAndPay(CreateOrderAndPayViewModel model)
    {
        if (model.PayFormWallet)
        {
            //todo: pay order price form wallet
        }

        var userId = User.Identity.GetLoggedInUserId();
        var address = await _addressService.GetAddressForCreateOrderAndPay(userId);
        // آیا کاربر آدرس داره ؟
        if (!address.HasUserAddress)
        {
            return Json(new JsonResultOperation(false));
        }

        var orderToAdd = new Entities.Order()
        {
            UserId = userId,
            AddressId = address.AddressId,
            PayFromWallet = false,
            OrderNumber = await _orderService.GetOrderNumberForCreateOrderAndPay()
        };

        // محصولات داخل سبد خرید کاربر
        var cartItems = await _cartService.GetCartsForCreateOrderAndPay(userId);

        // ارسال عادی
        var normalProducts = cartItems
            .Where(x => x.ProductVariantProductDimension == Dimension.Normal)
            .ToList();

        // ارسال کالاهای سنگین
        var heavyProducts = cartItems
            .Where(x => x.ProductVariantProductDimension == Dimension.Heavy)
            .ToList();

        // ارسال کالاهای فوق سنگین
        var ultraHeavyProducts = cartItems
            .Where(x => x.ProductVariantProductDimension == Dimension.UltraHeavy)
            .ToList();

        // قیمت کل کالاهایی که ابعادشان عادی است
        var sumPriceOfNormalProducts = normalProducts
            .Sum(x =>
                (x.IsDiscountActive ? x.ProductVariantOffPrice.Value : x.ProductVariantPrice)
                *
                x.Count
            );

        // قیمت کل کالاهایی که ابعادشان سنگین است
        var sumPriceOfHeavyProducts = heavyProducts
            .Sum(x =>
                (x.IsDiscountActive ? x.ProductVariantOffPrice.Value : x.ProductVariantPrice)
                *
                x.Count
            );
        
        if (normalProducts.Count > 0)
        {
            // مرسوله
            var parcelPostToAdd = new Entities.ParcelPost()
            {
                Dimension = Dimension.Normal,
                Status = OrderStatus.WaitingForPaying,
                ShippingPrice = sumPriceOfNormalProducts < 500000 ? 30000 : 0
            };

            // محتوای داخل مرسوله
            foreach (var normalProduct in normalProducts)
            {
                var parcelPostItemToAdd = new Entities.ParcelPostItem()
                {
                    Count = normalProduct.Count,
                    ProductVariantId = normalProduct.ProductVariantId,
                    GuaranteeId = normalProduct.ProductVariantGuaranteeId,
                    Score = normalProduct.Score,
                    Price = normalProduct.ProductVariantPrice
                };
                if (normalProduct.IsDiscountActive)
                    parcelPostItemToAdd.DiscountPrice =
                        normalProduct.ProductVariantPrice - normalProduct.ProductVariantOffPrice.Value;
                parcelPostToAdd.ParcelPostItems.Add(parcelPostItemToAdd);
            }
            orderToAdd.ParcelPosts.Add(parcelPostToAdd);
        }

        if (heavyProducts.Count > 0)
        {
            // مرسوله
            var parcelPostToAdd = new Entities.ParcelPost()
            {
                Dimension = Dimension.Normal,
                Status = OrderStatus.WaitingForPaying,
                ShippingPrice = sumPriceOfNormalProducts < 500000 ? 45000 : 0
            };

            // محتوای داخل مرسوله
            foreach (var heavyProduct in heavyProducts)
            {
                var parcelPostItemToAdd = new Entities.ParcelPostItem()
                {
                    Count = heavyProduct.Count,
                    ProductVariantId = heavyProduct.ProductVariantId,
                    GuaranteeId = heavyProduct.ProductVariantGuaranteeId,
                    Score = heavyProduct.Score,
                    Price = heavyProduct.ProductVariantPrice
                };
                if (heavyProduct.IsDiscountActive)
                    parcelPostItemToAdd.DiscountPrice =
                        heavyProduct.ProductVariantPrice - heavyProduct.ProductVariantOffPrice.Value;
                parcelPostToAdd.ParcelPostItems.Add(parcelPostItemToAdd);
            }
            orderToAdd.ParcelPosts.Add(parcelPostToAdd);
        }

        if (ultraHeavyProducts.Count > 0)
        {
            // مرسوله
            var parcelPostToAdd = new Entities.ParcelPost()
            {
                Dimension = Dimension.UltraHeavy,
                Status = OrderStatus.WaitingForPaying,
                ShippingPrice = 0
            };

            // محتوای داخل مرسوله
            foreach (var ultraHeavyProduct in ultraHeavyProducts)
            {
                var parcelPostItemToAdd = new Entities.ParcelPostItem()
                {
                    Count = ultraHeavyProduct.Count,
                    ProductVariantId = ultraHeavyProduct.ProductVariantId,
                    GuaranteeId = ultraHeavyProduct.ProductVariantGuaranteeId,
                    Score = ultraHeavyProduct.Score,
                    Price = ultraHeavyProduct.ProductVariantPrice
                };
                if (ultraHeavyProduct.IsDiscountActive)
                    parcelPostItemToAdd.DiscountPrice =
                        ultraHeavyProduct.ProductVariantPrice - ultraHeavyProduct.ProductVariantOffPrice.Value;
                parcelPostToAdd.ParcelPostItems.Add(parcelPostItemToAdd);
            }
            orderToAdd.ParcelPosts.Add(parcelPostToAdd);
        }

        await _orderService.AddAsync(orderToAdd);
        await _uow.SaveChangesAsync();

        return Json(new JsonResultOperation(true, "اوکی")
        {
            Data = "test data"
        });
    }
}