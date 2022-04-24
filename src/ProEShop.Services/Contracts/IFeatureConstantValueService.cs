using ProEShop.Entities;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.FeatureConstantValues;

namespace ProEShop.Services.Contracts;

public interface IFeatureConstantValueService : IGenericService<FeatureConstantValue>
{
    Task<ShowFeatureConstantValuesViewModel> GetFeatureConstantValues(ShowFeatureConstantValuesViewModel model);
}