using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.ProductVariants;

namespace ProEShop.Services.Contracts;

public interface IProductVariantService : IGenericService<ProductVariant>
{
    Task<List<ShowProductVariantViewModel>> GetProductVariants(long productId);

    /// <summary>
    /// گرفتن آخرین کد تنوع محصول به علاوه یک
    /// جهت استفاده در صفحه افزودن تنوع محصول
    /// </summary>
    /// <returns></returns>
    Task<int> GetVariantCodeForCreateProductVariant();

    /// <summary>
    /// گرفتن اطلاعات مربوط به تنوع محصول
    /// جهت استفاده در صفحه ایجاد محموله
    /// </summary>
    /// <param name="variantCode"></param>
    /// <returns></returns>
    Task<ShowProductVariantInCreateConsignmentViewModel> GetProductVariantForCreateConsignment(int variantCode);
}