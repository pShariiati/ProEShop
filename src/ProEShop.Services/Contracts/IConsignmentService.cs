using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Services.Contracts;

public interface IConsignmentService : IGenericService<Consignment>
{
    Task<ShowConsignmentsViewModel> GetConsignments(ShowConsignmentsViewModel model);
}