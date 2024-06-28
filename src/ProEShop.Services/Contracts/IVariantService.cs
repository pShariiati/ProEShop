using ProEShop.Entities;
using ProEShop.ViewModels.Variants;

namespace ProEShop.Services.Contracts;

public interface IVariantService : IGenericService<Variant>
{
    Task<ShowVariantsViewModel> GetVariants(ShowVariantsViewModel model);

    /// <summary>
    /// استفاده شده در صفحه شما هم فروشنده شوید
    /// این متود سه کار رو انجام میده
    /// آیا محصولی که وارد کرده وجود داره ؟
    /// آیا تنوعی که وارد کرده وجود داره ؟
    /// اگه تنوع وارد شده رنگ هست
    /// باید تنوع محصول هم رنگ باشه
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="variantId"></param>
    /// <returns></returns>
    Task<(bool IsSuccessful, bool IsVariantNull)> CheckProductAndVariantTypeForForAddVariant(long productId, long variantId);

    /// <summary>
    /// گرفتن لیست تمامی تنوع ها
    /// استفاده شده در صفحه ویرایش تنوع دسته بندی بخش ادمین
    /// </summary>
    /// <param name="isColor"></param>
    /// <returns></returns>
    Task<List<ShowVariantInEditCategoryVariantViewModel>> GetVariantsForEditCategoryVariants(bool isColor);

    // آیا تنوع هایی که قراره برای این دسته بندی اضافه شه
    // آیدیشون به درستی وارد شده
    // و اگر تنوع این دسته بندی رنگ باشد
    // باید توسط ادمین فقط رنگ به سمت سرور اومده باشد
    Task<bool> CheckVariantsCountAndConfirmStatusForEditCategoryVariants(List<long> variantsIds, bool isColor);
}