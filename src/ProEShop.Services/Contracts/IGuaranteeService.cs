using ProEShop.Entities;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Guarantees;
using ProEShop.ViewModels.Variants;

namespace ProEShop.Services.Contracts;

public interface IGuaranteeService : IGenericService<Guarantee>
{
    Task<ShowGuaranteesViewModel> GetGuarantees(ShowGuaranteesViewModel model);

    Task<List<ShowSelect2DataByAjaxViewModel>> SearchOnGuaranteesForSelect2(string input);
}