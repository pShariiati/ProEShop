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

    /// <summary>
    /// آیا محموله ایی با این آیدی وجود دارد
    /// که وضعیت آن دریافت شده باشد
    /// جهت استفاده در صفحه افزایش موجودی و ثبت نظر محموله
    /// </summary>
    /// <param name="consignmentId"></param>
    /// <returns></returns>
    Task<bool> IsExistsConsignmentWithReceivedStatus(long consignmentId);

    /// <summary>
    /// گرفتن محموله برای تغییر وضعیت آن به
    /// "دریافت شده و افزایش موجودی"
    /// برای محموله نظر ثبت میشه و بعد از اون موجودی تنوع محصولات رو افزایش میدیم
    /// </summary>
    /// <param name="consignmentId"></param>
    /// <returns></returns>
    Task<Consignment> GetConsignmentWithReceivedStatus(long consignmentId);
}