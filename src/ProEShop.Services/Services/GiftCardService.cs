using AutoMapper;
using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.GiftCards;

namespace ProEShop.Services.Services;

public class GiftCardService : GenericService<GiftCard>, IGiftCardService
{
    private readonly DbSet<GiftCard> _giftCards;
    private readonly IMapper _mapper;
    private readonly ICartService _cartService;
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    private readonly IParcelPostItemService _parcelPostItemService;

    public GiftCardService(
        IUnitOfWork uow,
        IMapper mapper,
        ICartService cartService,
        ICategoryService categoryService,
        IBrandService brandService,
        IParcelPostItemService parcelPostItemService)
        : base(uow)
    {
        _mapper = mapper;
        _cartService = cartService;
        _categoryService = categoryService;
        _brandService = brandService;
        _parcelPostItemService = parcelPostItemService;
        _giftCards = uow.Set<GiftCard>();
    }

    public async Task<CheckGiftCardCodeForPaymentViewModel> CheckForGiftCardPriceForPayment(GetGiftCardCodeDataViewModel model, bool showGiftCardId)
    {
        var giftCard = await _giftCards
            .Include(x => x.Order)
            .SingleOrDefaultAsync(x => x.Code == model.GiftCardCode);

        if (giftCard is null)
        {
            return new(false, default, "کارت هدیه پیدا نشد");
        }

        if (giftCard.Order != null)
        {
            return new (false, default, "کارت هدیه قبلا استفاده شده است");
        }

        if (giftCard.EndDateTime != null)
        {
            var now = DateTime.Now;

            if (now > giftCard.EndDateTime)
            {
                return new(false, default, "کد تخفیف منقضی شده است");
            }
        }

        if (giftCard.BrandId != null)
        {
            if (!await _cartService.CheckBrandIdForExistingInCart(giftCard.BrandId.Value))
            {
                var brandTitle = await _brandService.GetBrandTitle(giftCard.BrandId.Value);

                return new(false, default,
                    $"در داخل سبد خرید باید حداقل یک محصول از برند {brandTitle} وجود داشته باشد");
            }
        }

        if (giftCard.CategoryId != null)
        {
            if (!await _cartService.CheckCategoryIdForExistingInCart(giftCard.CategoryId.Value))
            {
                var categoryTitle = await _categoryService.GetCategoryTitle(giftCard.CategoryId.Value);

                return new(false, default,
                    $"در داخل سبد خرید باید حداقل یک محصول از دسته بندی {categoryTitle} وجود داشته باشد");
            }
        }

        if (giftCard.MinimumPriceOfCart > model.SumPriceOfCart)
        {
            return new(false, default,
                $"مجموع قیمت محصولات داخل سبد خرید باید  مساوی و یا بیشتر از {giftCard.MinimumPriceOfCart.Value.ToString("#,0").ToPersianNumbers()} باشد");
        }

        if (showGiftCardId)
        {
            return new(true, giftCard.Price, null, giftCard.Id);
        }
        return new(true, giftCard.Price);
    }

    public async Task<(bool Result, string Message)> CheckForGiftCardCodeInVerify(Order order)
    {
        var giftCard = await _giftCards
            .Include(x => x.Order)
            .SingleAsync(x => x.Id == order.ReservedGiftCardId);

        if (giftCard.Order != null)
        {
            return (false,
                "متاسفانه قبل از اینکه پرداخت خود را تکمیل کنید، از کارت هدیه شما استفاده شد و دیگر امکان استفاده از آن را ندارید");
        }

        if (giftCard.EndDateTime != null)
        {
            var now = DateTime.Now;

            if (now > giftCard.EndDateTime)
            {
                return (false, $"متاسفانه مهلت زمانی کارت هدیه {giftCard.Code} در زمانیکه پرداخت خود را انجام میدادید به پایان رسیده است");
            }
        }

        if (order.GiftCardCodePrice != giftCard.Price)
        {
            return (false,
                $"متاسفانه مبلغ کارت هدیه {giftCard.Code} در زمانیکه پرداخت خود را انجام میدادید به میزان {giftCard.Price.ToString("#,0").ToPersianNumbers()} تومان تغییر پیدا کرده است");
        }

        if (giftCard.BrandId != null)
        {
            if (!await _parcelPostItemService.CheckBrandIdForExistingInOrder(order.Id, giftCard.BrandId.Value))
            {
                var brandTitle = await _brandService.GetBrandTitle(giftCard.BrandId.Value);
                return (false,
                    $"متاسفانه کارت هدیه {giftCard.Code} در زمانیکه پرداخت خود را انجام میدادید، برند آن تغییر پیدا کرده است، در حال حاضر برای استفاده از این کارت باید یک محصول از برند {brandTitle} در داخل سفارش شما وجود داشته باشد");
            }
        }

        if (giftCard.CategoryId != null)
        {
            if (!await _parcelPostItemService.CheckCategoryIdForExistingInOrder(order.Id, giftCard.CategoryId.Value))
            {
                var categoryTitle = await _categoryService.GetCategoryTitle(giftCard.CategoryId.Value);

                return (false,
                    $"متاسفانه کارت هدیه {giftCard.Code} در زمانیکه پرداخت خود را انجام میدادید، دسته بندی آن تغییر پیدا کرده است، در حال حاضر برای استفاده از این کارت باید یک محصول از دسته بندی {categoryTitle} در داخل سفارش شما وجود داشته باشد");
            }
        }

        if (giftCard.MinimumPriceOfCart != null)
        {
            if (giftCard.MinimumPriceOfCart > order.FinalPrice + order.DiscountPrice + order.GiftCardCodePrice)
            {
                return (false, $"متاسفانه حداقل مبلغ سبد خرید برای استفاده از کارت هدیه {giftCard.Code} به مبلغ {giftCard.MinimumPriceOfCart.Value.ToString("#,0").ToPersianNumbers()} تومان تغییر پیدا کرده است");
            }
        }

        return (true, default);
    }
}