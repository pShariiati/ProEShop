using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.ConsignmentItems;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Services.Contracts;

public interface IConsignmentItemService : IGenericService<ConsignmentItem>
{
    Task<List<ShowConsignmentItemViewModel>> GetConsignmentItems(long consignmentId);
}