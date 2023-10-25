using ProEShop.Entities;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Services.Contracts;

public interface IReturnProductItemService : IGenericService<ReturnProductItem>
{
    /// <summary>
    /// گرفتن محصولات داخل یک ثبت مرجوعی
    /// </summary>
    /// <param name="returnProductId"></param>
    /// <returns></returns>
    Task<List<ReturnProductItemViewModel>> GetItemsByReturnProductId(long returnProductId);
}