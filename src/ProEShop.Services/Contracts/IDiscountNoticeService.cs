using ProEShop.Entities;
using ProEShop.ViewModels.CategoryFeatures;
using ProEShop.ViewModels.DiscountNotices;

namespace ProEShop.Services.Contracts;

public interface IDiscountNoticeService : ICustomGenericService<DiscountNotice>
{
    /// <summary>
    /// گرفتن اطلاعات برای بخش اطلاع رسانی شگفت انگیز
    /// برای مثال اگه اطلاع رسانی از طریق شماره تلفن رو از قبل فعال کرده باشد
    /// باید چکباکس مربوطه رو تیک بزنیم
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AddDiscountNoticeViewModel> GetDataForAddDiscountNotice(long productId, long userId);
}