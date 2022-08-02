using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
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
    Task<bool> CheckProductAndVariantTypeForForAddVariant(long productId, long variantId);
}