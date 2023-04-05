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
    private readonly IUsedDiscountCodeService _usedDiscountCodeService;
    private readonly IParcelPostItemService _parcelPostItemService;

    public DiscountCodeService(
        IUnitOfWork uow,
        IMapper mapper,
        IBrandService brandService,
        ICartService cartService,
        ICategoryService categoryService,
        IUsedDiscountCodeService usedDiscountCodeService,
        IParcelPostItemService parcelPostItemService)
        : base(uow)
    {
        _mapper = mapper;
        _brandService = brandService;
        _cartService = cartService;
        _categoryService = categoryService;
        _usedDiscountCodeService = usedDiscountCodeService;
        _parcelPostItemService = parcelPostItemService;
        _discountCodes = uow.Set<DiscountCode>();
    }

    public async Task<CheckDiscountCodeForPaymentViewModel> CheckForDiscountPriceForPayment(GetDiscountCodeDataViewModel model, bool showDiscountCodeId)
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

        if (discountCode.LimitedCount != null)
        {
            var usedDiscountCodeCount = await _usedDiscountCodeService.GetCountOfUsedDiscount(discountCode.Id);

            // چرا از بزرگتر مساوی استفاده میکنم
            // لیمیتت کانت پنج تاست
            // سه نفر از کد تخفیف استفاده میکنن
            // بعد لیمتت رو ویرایش میکنیم به 2
            // حالا شرط پایین برقرار میشه و چون استفاده شده (3) بزرگتر از لیمتت هست (2) در نتیجه
            // متن خطا رو نمایش میده
            // در صورتیکه اگر از مساوی برای شرط پایین استفاده میکردیم، اصلا خطا رو نمایش نمیداد
            if (usedDiscountCodeCount >= discountCode.LimitedCount)
            {
                return new(false, default, "ظرفیت استفاده از کد تخفیف به اتمام رسیده است");
            }
        }

        if (discountCode.LimitedCountByOneUser != null)
        {
            var usedDiscountCodeCountByCurrentUser = await _usedDiscountCodeService.GetCountOfUsedDiscountByOneUser(discountCode.Id);

            // چرا از بزرگتر مساوی استفاده میکنیم
            // لیمیتت کانت کاربر پنج تاست
            // سه بار از کد تخفیف استفاده میکنه
            // بعد لیمتت رو ویرایش میکنیم به 2
            // حالا شرط پایین برقرار میشه و چون استفاده شده (3) بزرگتر از لیمتت کاربر هست (2) در نتیجه
            // متن خطا رو نمایش میده
            // در صورتیکه اگر از مساوی برای شرط پایین استفاده میکردیم، اصلا خطا رو نمایش نمیداد
            if (usedDiscountCodeCountByCurrentUser >= discountCode.LimitedCountByOneUser)
            {
                return new(false, default, $"ظرفیت استفاده از این کد تخفیف برای هر کاربر {discountCode.LimitedCountByOneUser} بار است و شما حداکثر استفاده از این کد تخفیف را داشته اید");
            }
        }

        if (showDiscountCodeId)
        {
            return new(true, discountCode.Price, null, discountCode.Id);
        }
        return new(true, discountCode.Price);
    }

    public async Task<(bool Result, string Message)> CheckForDiscountCodeInVerify(Order order)
    {
        var discountCode = await _discountCodes.SingleAsync(x => x.Id == order.DiscountCodeId);

        var now = DateTime.Now;

        if (discountCode.LimitedCount != null)
        {
            var usedDiscountCodeCount = await _usedDiscountCodeService.GetCountOfUsedDiscount(discountCode.Id);

            if (usedDiscountCodeCount >= discountCode.LimitedCount)
            {
                return (false, $"متاسفانه ظرفیت استفاده از کد تخفیف {discountCode.Code} در زمانیکه پرداخت خود را انجام میدادید به پایان رسید");
            }
        }

        if (discountCode.StartDateTime > now)
        {
            return (false,
                $"متاسفانه زمان شروع کد تخفیف در زمانیکه پرداخت خود را انجام میدادید تغییر پیدا کرده است و در حال حاضر امکان استفاده از کد تخفیف {discountCode.Code} وجود ندارد");
        }

        if (now > discountCode.EndDateTime)
        {
            return (false,
                $"متاسفانه مهلت زمانی کد تخفیف {discountCode.Code} در زمانیکه پرداخت خود را انجام میدادید به پایان رسیده است");
        }

        if (order.DiscountCodePrice != discountCode.Price)
        {
            return (false,
                $"متاسفانه مبلغ کد تخفیف {discountCode.Code} در زمانیکه پرداخت خود را انجام میدادید به میزان {discountCode.Price.ToString("#,0").ToPersianNumbers()} تومان تغییر پیدا کرده است");
        }

        if (discountCode.MinimumPriceOfCart != null)
        {
            if (discountCode.MinimumPriceOfCart > order.FinalPrice + order.DiscountCodePrice + order.GiftCardCodePrice)
            {
                return (false, $"متاسفانه حداقل مبلغ سبد خرید برای استفاده از کد تخفیف {discountCode.Code} به مبلغ {discountCode.MinimumPriceOfCart.Value.ToString("#,0").ToPersianNumbers()} تومان تغییر پیدا کرده است");
            }
        }

        if (discountCode.BrandId != null)
        {
            if (!await _parcelPostItemService.CheckBrandIdForExistingInOrder(order.Id, discountCode.BrandId.Value))
            {
                var brandTitle = await _brandService.GetBrandTitle(discountCode.BrandId.Value);
                return (false,
                    $"متاسفانه کد تخفیف {discountCode.Code} در زمانیکه پرداخت خود را انجام میدادید، برند آن تغییر پیدا کرده است، در حال حاضر برای استفاده از این کد تخفیف باید یک محصول از برند {brandTitle} در داخل سفارش شما وجود داشته باشد");
            }
        }

        if (discountCode.CategoryId != null)
        {
            if (!await _parcelPostItemService.CheckCategoryIdForExistingInOrder(order.Id, discountCode.CategoryId.Value))
            {
                var categoryTitle = await _categoryService.GetCategoryTitle(discountCode.CategoryId.Value);
                return (false,
                    $"متاسفانه کد تخفیف {discountCode.Code} در زمانیکه پرداخت خود را انجام میدادید، دسته بندی آن تغییر پیدا کرده است، در حال حاضر برای استفاده از این کد تخفیف باید یک محصول از دسته بندی {categoryTitle} در داخل سفارش شما وجود داشته باشد");
            }
        }

        return (true, default);
    }
}