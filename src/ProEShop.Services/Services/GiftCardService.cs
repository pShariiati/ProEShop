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

    public GiftCardService(
        IUnitOfWork uow,
        IMapper mapper,
        ICartService cartService,
        ICategoryService categoryService,
        IBrandService brandService)
        : base(uow)
    {
        _mapper = mapper;
        _cartService = cartService;
        _categoryService = categoryService;
        _brandService = brandService;
        _giftCards = uow.Set<GiftCard>();
    }

    public async Task<CheckGiftCardCodeForPaymentViewModel> CheckForGiftCardPriceForPayment(GetGiftCardCodeDataViewModel model, bool showGiftCardId)
    {
        var giftCard = await _giftCards.SingleOrDefaultAsync(x => x.Code == model.GiftCardCode);

        if (giftCard is null)
        {
            return new(false, default, "کارت هدیه پیدا نشد");
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
}