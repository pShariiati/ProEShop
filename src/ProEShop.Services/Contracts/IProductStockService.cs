using ProEShop.Entities;
using ProEShop.ViewModels.Brands;

namespace ProEShop.Services.Contracts;

public interface IProductStockService : IGenericService<ProductStock>
{
    /// <summary>
    /// آیا در داخل جدول موجودی کالا برای این تنوع محصول و در این محموله رکوردی وجود دارد ؟
    /// جهت استفاده در صفحه افزایش موجودی کالا
    /// اگر وجود داشته باشد باید همین رکورد را ویرایش کنیم و تعداد
    /// Count
    /// آنرا تغییر دهیم. چرا ؟
    /// چون در یک محموله نمیشه دو تا تنوع محصول همسان وجود داشته باشن
    /// و ما فقط باید موجودی را افزایش دهیم
    /// </summary>
    /// <param name="productVariantId"></param>
    /// <param name="consignmentId"></param>
    /// <returns></returns>
    Task<ProductStock> GetByProductVariantIdAndConsignmentId(long productVariantId, long consignmentId);
}