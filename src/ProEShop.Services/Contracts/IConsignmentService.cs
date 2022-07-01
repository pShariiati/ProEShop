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

    Task<ShowConsignmentDetailsViewModel> GetConsignmentDetails(long consignmentId);
}