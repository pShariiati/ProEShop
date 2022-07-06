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

    /// <summary>
    /// خواندن موجودی کالا ها برای افزایش موجودی در جدول تنوع محصولات
    /// برای اینکه همیشه به جدول موجودی کالا ها کوئری نزنیم که بفهمیم موجودی کالا چقدر هستش
    /// این مورد رو به جدول تنوع محصولات اضافه میکنیم که موجودی کالا همیشه در دسترس باشه
    /// این متد در صفحه ثبت نظر برای محموله استفاده میشه
    /// </summary>
    /// <param name="consignmentId"></param>
    /// <returns></returns>
    Task<Dictionary<long, int>> GetProductStocksForAddProductVariantsCount(long consignmentId);
}