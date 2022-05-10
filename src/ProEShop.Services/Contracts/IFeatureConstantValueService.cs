using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.FeatureConstantValues;

namespace ProEShop.Services.Contracts;

public interface IFeatureConstantValueService : IGenericService<FeatureConstantValue>
{
    Task<ShowFeatureConstantValuesViewModel> GetFeatureConstantValues(ShowFeatureConstantValuesViewModel model);
    Task<List<ShowCategoryFeatureConstantValueViewModel>> GetFeatureConstantValuesByCategoryId(long categoryId);

    /// <summary>
    /// آیا ویژگی وارد شده جزء ویژگی های غیر ثابت هست یا خیر ؟
    /// اگر آیدی یکی از فیچر های غیر ثابت محصول، در جدول ویژگی های ثابت وجود داشت
    /// باید به کاربر اخطار نمایش دهیم
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="featureIds"></param>
    /// <returns></returns>
    Task<bool> CheckNonConstantValue(long categoryId, List<long> featureIds);

    /// <summary>
    /// آیا به همان تعداد که مقادیر ثابت وجود دارد
    /// به همان میزان هم مقادیر ثابت توسط فروشنده به سمت سرور آمده است ؟
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="featureConstantValueIds"></param>
    /// <returns></returns>
    Task<bool> CheckConstantValue(long categoryId, List<long> featureConstantValueIds);
}