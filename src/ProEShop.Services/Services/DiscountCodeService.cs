using AutoMapper;
using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.DiscountCodes;

namespace ProEShop.Services.Services;

public class DiscountCodeService : GenericService<DiscountCode>, IDiscountCodeService
{
    private readonly DbSet<DiscountCode> _discountCodes;
    private readonly IMapper _mapper;
    private readonly IBrandService _brandService;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;

    public DiscountCodeService(
        IUnitOfWork uow,
        IMapper mapper,
        IBrandService brandService,
        ICartService cartService,
        ICategoryService categoryService)
        : base(uow)
    {
        _mapper = mapper;
        _brandService = brandService;
        _cartService = cartService;
        _categoryService = categoryService;
        _discountCodes = uow.Set<DiscountCode>();
    }

    public async Task<CheckDiscountCodeViewModel> CheckForDiscountPrice(GetDiscountCodeDataViewModel model)
    {
        var discountCode = await _discountCodes.SingleOrDefaultAsync(x => x.Code == model.DiscountCode);

        if (discountCode is null)
        {
            return new(false, default, "کد تخفیف پیدا نشد");
        }

        var now = DateTime.Now;

        if (discountCode.StartDateTime > now)
        {
            return new(false, default, "زمان کد تخفیف شروع نشده است");
        }

        if (now > discountCode.EndDateTime)
        {
            return new(false, default, "کد تخفیف منقضی شده است");
        }

        if (discountCode.BrandId != null)
        {
            if (!await _cartService.CheckBrandIdForExistingInCart(discountCode.BrandId.Value))
            {
                var brandTitle = await _brandService.GetBrandTitle(discountCode.BrandId.Value);

                return new(false, default,
                    $"در داخل سبد خرید باید حداقل یک محصول از برند {brandTitle} وجود داشته باشد");
            }
        }

        if (discountCode.CategoryId != null)
        {
            if (!await _cartService.CheckCategoryIdForExistingInCart(discountCode.CategoryId.Value))
            {
                var categoryTitle = await _categoryService.GetCategoryTitle(discountCode.CategoryId.Value);

                return new(false, default,
                    $"در داخل سبد خرید باید حداقل یک محصول از دسته بندی {categoryTitle} وجود داشته باشد");
            }
        }

        if (discountCode.MinimumPriceOfCart > model.SumPriceOfCart)
        {
            return new(false, default,
                $"مجموع قیمت محصولات داخل سبد خرید باید  مساوی و یا بیشتر از {discountCode.MinimumPriceOfCart.Value.ToString("#,0").ToPersianNumbers()} باشد");
        }

        return new(true, discountCode.Price);
    }

    public async Task<CheckDiscountCodeForPaymentViewModel> CheckForDiscountPriceForPayment(GetDiscountCodeDataViewModel model)
    {
        var discountCode = await _discountCodes.SingleOrDefaultAsync(x => x.Code == model.DiscountCode);

        if (discountCode is null)
        {
            return new(false, default, "کد تخفیف پیدا نشد");
        }

        var now = DateTime.Now;

        if (discountCode.StartDateTime > now)
        {
            return new(false, default, "زمان کد تخفیف شروع نشده است");
        }

        if (now > discountCode.EndDateTime)
        {
            return new(false, default, "کد تخفیف منقضی شده است");
        }

        if (discountCode.BrandId != null)
        {
            if (!await _cartService.CheckBrandIdForExistingInCart(discountCode.BrandId.Value))
            {
                var brandTitle = await _brandService.GetBrandTitle(discountCode.BrandId.Value);

                return new(false, default,
                    $"در داخل سبد خرید باید حداقل یک محصول از برند {brandTitle} وجود داشته باشد");
            }
        }

        if (discountCode.CategoryId != null)
        {
            if (!await _cartService.CheckCategoryIdForExistingInCart(discountCode.CategoryId.Value))
            {
                var categoryTitle = await _categoryService.GetCategoryTitle(discountCode.CategoryId.Value);

                return new(false, default,
                    $"در داخل سبد خرید باید حداقل یک محصول از دسته بندی {categoryTitle} وجود داشته باشد");
            }
        }

        if (discountCode.MinimumPriceOfCart > model.SumPriceOfCart)
        {
            return new(false, default,
                $"مجموع قیمت محصولات داخل سبد خرید باید  مساوی و یا بیشتر از {discountCode.MinimumPriceOfCart.Value.ToString("#,0").ToPersianNumbers()} باشد");
        }

        return new(true, discountCode.Price, null, discountCode.Id);
    }
}