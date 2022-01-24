using ProEShop.Entities;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Features;

namespace ProEShop.Services.Contracts;

public interface IProvinceAndCityService : ICustomGenericService<ProvinceAndCity>
{
    Task<Dictionary<long, string>> GetProvincesToShowInSelectBoxAsync();
}