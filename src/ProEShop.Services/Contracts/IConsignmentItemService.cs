using ProEShop.Entities;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Services.Contracts;

public interface IConsignmentItemService : IGenericService<ConsignmentItem>
{
    /// <summary>
    /// آیا رکوردی با این تنوع محصول، در آیتم های این محموله وجود دارد یا خیر ؟
    /// جهت استفاده در هنگام افزودن موجودی بر اساس محموله
    /// </summary>
    /// <param name="productVariantId"></param>
    /// <param name="consignmentId"></param>
    /// <returns></returns>
    Task<bool> IsExistsByProductVariantIdAndConsignmentId(long productVariantId, long consignmentId);
}