using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Services.Contracts;

public interface IConsignmentService : IGenericService<Consignment>
{
    Task<ShowConsignmentsViewModel> GetConsignments(ShowConsignmentsViewModel model);

    /// <summary>
    /// خواندن یک محموله جهت تایید کردن آن
    /// در بخش جزییات محموله پنل انبارداری از این متود استفاده میکنیم
    /// </summary>
    /// <param name="consignmentId"></param>
    /// <returns></returns>
    Task<Consignment> GetConsignmentForConfirmation(long consignmentId);


    /// <summary>
    /// گرفتن جزییات محموله جهت استفاده در صفحه مدیریت محموله ها
    /// در پنل انبارداری
    /// </summary>
    /// <param name="consignmentId"></param>
    /// <returns></returns>
    Task<ShowConsignmentDetailsViewModel> GetConsignmentDetails(long consignmentId);

    /// <summary>
    ///  گرفتن محموله برای برای تغییر وضعیت آن به دریافت شده
    /// استفاده شده در صفحه مدیریت محموله ها
    /// در پنل انبارداری
    /// </summary>
    /// <param name="consignmentId"></param>
    /// <returns></returns>
    Task<Consignment> GetConsignmentToChangeStatusToReceived(long consignmentId);
}