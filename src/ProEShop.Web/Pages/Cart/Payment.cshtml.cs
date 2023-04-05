using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parbad;
using Parbad.AspNetCore;
using Parbad.Gateway.ZarinPal;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.DiscountCodes;
using ProEShop.ViewModels.GiftCards;

namespace ProEShop.Web.Pages.Cart;

[Authorize]
public class PaymentModel : PageBase
{
    #region Constructor

    private readonly ICartService _cartService;
    private readonly IAddressService _addressService;
    private readonly IOrderService _orderService;
    private readonly IUnitOfWork _uow;
    private readonly IOnlinePayment _onlinePayment;
    private readonly IDiscountCodeService _discountCodeService;
    private readonly IGiftCardService _giftCardService;

    public PaymentModel(
        ICartService cartService,
        IAddressService addressService,
        IOrderService orderService,
        IUnitOfWork uow,
        IOnlinePayment onlinePayment,
        IDiscountCodeService discountCodeService,
        IGiftCardService giftCardService)
    {
        _cartService = cartService;
        _addressService = addressService;
        _orderService = orderService;
        _uow = uow;
        _onlinePayment = onlinePayment;
        _discountCodeService = discountCodeService;
        _giftCardService = giftCardService;
    }

    #endregion

    public PaymentViewModel PaymentPage { get; set; }
        = new();

    [BindProperty]
    public CreateOrderAndPayViewModel CreateOrderAndPayModel { get; set; }

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

    /// <summary>
    /// ایجاد سفارش و انتقال کاربر به درگاه بانکی
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> OnPost()
    {
        var userId = User.Identity.GetLoggedInUserId();

        if (!ModelState.IsValid)
        {
            PaymentPage.CartItems = await _cartService.GetCartsForPaymentPage(userId);

            // اگر سبد خرید خالی بود، کاربر رو به صفحه سبد خرید انتقال بده
            if (PaymentPage.CartItems.Count < 1)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        // پرداخت هزینه سفارش از کیف پول کاربر
        if (CreateOrderAndPayModel.PayFormWallet)
        {
            //todo: pay order price form wallet
        }
        
        var address = await _addressService.GetAddressForCreateOrderAndPay(userId);
        // آیا کاربر آدرس داره ؟
        if (!address.HasUserAddress)
        {
            return RedirectToPage("Checkout");
        }

        var orderToAdd = new Entities.Order()
        {
            UserId = userId,
            AddressId = address.AddressId,
            PayFromWallet = false,
            Status = OrderStatus.WaitingForPaying
        };

        // محصولات داخل سبد خرید کاربر
        var cartItems = await _cartService.GetCartsForCreateOrderAndPay(userId);

        if (cartItems.Count < 1)
        {
            return RedirectToPage("Index");
        }

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
                Status = ParcelPostStatus.WaitingForPaying,
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
                    Price = normalProduct.ProductVariantPrice,
                    Order = orderToAdd
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
                Dimension = Dimension.Heavy,
                Status = ParcelPostStatus.WaitingForPaying,
                ShippingPrice = sumPriceOfHeavyProducts < 500000 ? 45000 : 0
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
                    Price = heavyProduct.ProductVariantPrice,
                    Order = orderToAdd
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
                Status = ParcelPostStatus.WaitingForPaying,
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
                    Price = ultraHeavyProduct.ProductVariantPrice,
                    Order = orderToAdd
                };
                if (ultraHeavyProduct.IsDiscountActive)
                    parcelPostItemToAdd.DiscountPrice =
                        ultraHeavyProduct.ProductVariantPrice - ultraHeavyProduct.ProductVariantOffPrice.Value;
                parcelPostToAdd.ParcelPostItems.Add(parcelPostItemToAdd);
            }
            orderToAdd.ParcelPosts.Add(parcelPostToAdd);
        }

        #region Empty user cart items

        // باید سبد خرید کاربر را خالی کنیم
        var cartItemsToRemove = new List<Entities.Cart>();

        foreach (var cartItem in cartItems)
        {
            cartItemsToRemove.Add(new Entities.Cart()
            {
                ProductVariantId = cartItem.ProductVariantId,
                UserId = userId
            });
        }

        //_cartService.RemoveRange(cartItemsToRemove);

        #endregion

        // قیمت نهایی بدون محاسبه تخفیف
        var totalPrice = cartItems
            .Sum(x => x.ProductVariantPrice * x.Count);

        // قیمت نهایی محصولات داخل سبد خرید کاربر با محاسبه تخفیف آنها
        var totalPriceWithDiscount = cartItems
            .Sum(x =>
                (x.IsDiscountActive ? x.ProductVariantOffPrice.Value : x.ProductVariantPrice)
                *
                x.Count
            );

        // مجموع امتیاز هایی که کاربر بعد از پایان مهلت مرجوعی به دست میاورد
        var sumScore = cartItems.Sum(x => x.Score);
        if (sumScore > 150)
            sumScore = 150;

        // این سفارش در چند مرسوله ارسال میشود ؟
        var shippingCount = 0;

        if (normalProducts.Count > 0)
            shippingCount++;
        if (heavyProducts.Count > 0)
            shippingCount++;
        if (ultraHeavyProducts.Count > 0)
            shippingCount++;

        // مجموع قیمت حمل و نقل مرسوله ها
        var sumPriceOfShipping = 0;

        if (sumPriceOfNormalProducts < 500000 && normalProducts.Count > 0)
        {
            sumPriceOfShipping += 30000;
        }

        if (sumPriceOfHeavyProducts < 500000 && heavyProducts.Count > 0)
        {
            sumPriceOfShipping += 45000;
        }

        // قیمت محصولات داخل سبد خرید کاربر به علاوه هزینه حمل و نقل مرسوله ها
        var finalPrice = totalPriceWithDiscount + sumPriceOfShipping;

        var discountCode = CreateOrderAndPayModel.DiscountCode;

        var discountCodePrice = 0;

        if (!string.IsNullOrWhiteSpace(discountCode))
        {
            var checkDiscountCode =
                await _discountCodeService.CheckForDiscountPriceForPayment(new(finalPrice, discountCode), true);
            if (!checkDiscountCode.Result)
            {
                PaymentPage.CartItems = await _cartService.GetCartsForPaymentPage(userId);

                // اگر سبد خرید خالی بود، کاربر رو به صفحه سبد خرید انتقال بده
                if (PaymentPage.CartItems.Count < 1)
                {
                    return RedirectToPage("Index");
                }

                ModelState.AddModelError(string.Empty, checkDiscountCode.Message);

                return Page();
            }

            orderToAdd.DiscountCodeId = checkDiscountCode.DiscountCodeId;
            orderToAdd.DiscountCodePrice = discountCodePrice = checkDiscountCode.DiscountPrice;
        }

        finalPrice = finalPrice - discountCodePrice <= 0 ? 0 : finalPrice - discountCodePrice;

        var giftCardCode = CreateOrderAndPayModel.GiftCardCode;

        var giftCardCodePrice = 0;

        if (!string.IsNullOrWhiteSpace(giftCardCode))
        {
            if (finalPrice == 0)
            {
                return RedirectToPage(PublicConstantStrings.Error500PageName);
            }
            var checkGiftCardCode =
                await _giftCardService.CheckForGiftCardPriceForPayment(new(finalPrice, giftCardCode), true);
            if (!checkGiftCardCode.Result)
            {
                PaymentPage.CartItems = await _cartService.GetCartsForPaymentPage(userId);

                // اگر سبد خرید خالی بود، کاربر رو به صفحه سبد خرید انتقال بده
                if (PaymentPage.CartItems.Count < 1)
                {
                    return RedirectToPage("Index");
                }

                ModelState.AddModelError(string.Empty, checkGiftCardCode.Message);

                return Page();
            }

            orderToAdd.ReservedGiftCardId = checkGiftCardCode.GiftCardId;
            orderToAdd.GiftCardCodePrice = giftCardCodePrice = checkGiftCardCode.DiscountPrice;
        }

        finalPrice = finalPrice - giftCardCodePrice <= 0 ? 0 : finalPrice - giftCardCodePrice;

        // کاربر بعد از درگاه به چه آدرسی هدایت شود
        var callbackUrl = Url.PageLink("VerifyPayment", null, null, Request.Scheme);

        var result = await _onlinePayment.RequestAsync(invoice =>
        {
            invoice
                .SetAmount(finalPrice)
                .SetCallbackUrl(callbackUrl)
                .SetGateway(CreateOrderAndPayModel.PaymentGateway.ToString())
                .UseAutoIncrementTrackingNumber();
            if (CreateOrderAndPayModel.PaymentGateway == PaymentGateway.Zarinpal)
            {
                invoice.SetZarinPalData(new ZarinPalInvoice("No description"));
            }
        });

        orderToAdd.OrderNumber = result.TrackingNumber;
        orderToAdd.PaymentGateway = CreateOrderAndPayModel.PaymentGateway;
        orderToAdd.TotalPrice = totalPrice;
        var discountPrice = totalPrice - totalPriceWithDiscount;

        // کل تخفیفات
        // چرا نمیشه از
        // +=
        // برای جمع بستن موارد پایین استفاده کرد ؟
        // چون نمیتوان از عملگر فوق برای
        // Int nullable
        // استفاده کرد
        // orderToAdd.DiscountPrice += discountCodePrice;
        // https://stackoverflow.com/questions/17943395/why-nullable-int-int-doesnt-increase-the-value-via-if-the-value-is-null
        if (discountPrice > 0)
        {
            // تخفیف خود کالاها
            orderToAdd.DiscountPrice = discountPrice;
        }

        if (discountCodePrice > 0)
        {
            // کد تخفیف
            orderToAdd.DiscountPrice = discountPrice + discountCodePrice;
            //orderToAdd.DiscountPrice += discountCodePrice;
        }

        if (giftCardCodePrice > 0)
        {
            // کارت هدیه
            orderToAdd.DiscountPrice = discountPrice + discountCodePrice + giftCardCodePrice;
        }

        orderToAdd.FinalPrice = finalPrice;
        orderToAdd.TotalScore = (byte)sumScore;
        orderToAdd.ShippingCount = (byte)shippingCount;

        if (finalPrice == 0)
        {
            // افزودن رکورد به جدولِ: کد های تخفیف استفاده شده
            if (orderToAdd.DiscountCodeId != null)
            {
                orderToAdd.UsedDiscountCodes.Add(new()
                {
                    UserId = userId,
                    DiscountCodeId = orderToAdd.DiscountCodeId.Value
                });
            }

            if (orderToAdd.ReservedGiftCardId != null)
            {
                orderToAdd.GiftCardId = orderToAdd.ReservedGiftCardId;
                orderToAdd.ReservedGiftCardId = null;
            }

            // وضعیت مرسوله های این سفارش را به حالت "در حال پردازش" تغییر میدهیم
            foreach (var parcelPost in orderToAdd.ParcelPosts)
            {
                parcelPost.Status = ParcelPostStatus.Processing;
            }

            orderToAdd.Status = OrderStatus.Processing;
            orderToAdd.IsPay = true;

            await _orderService.AddAsync(orderToAdd);
            await _uow.SaveChangesAsync();

            return RedirectToPage("VerifyPayment", new { orderNumber = orderToAdd.OrderNumber });
        }

        if (result.IsSucceed)
        {
            await _orderService.AddAsync(orderToAdd);
            await _uow.SaveChangesAsync();

            return result.GatewayTransporter.TransportToGateway();
        }

        return RedirectToPage(PublicConstantStrings.Error500PageName);
    }

    /// <summary>
    /// بررسی کد تخفیف که آیا وجود داره یا خیر
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetCheckForDiscount(GetDiscountCodeDataViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return JsonBadRequest();
        }

        var discountCodeResult = await _discountCodeService.CheckForDiscountPriceForPayment(model, false);

        return JsonOk(string.Empty, discountCodeResult);
    }

    /// <summary>
    /// بررسی کارت هدیه که آیا وجود داره یا خیر
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetCheckForGiftCard(GetGiftCardCodeDataViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return JsonBadRequest();
        }

        var giftCardCodeResult = await _giftCardService.CheckForGiftCardPriceForPayment(model, false);

        return JsonOk(string.Empty, giftCardCodeResult);
    }
}