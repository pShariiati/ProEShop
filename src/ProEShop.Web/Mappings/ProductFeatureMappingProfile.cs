using AutoMapper;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class ProductFeatureMappingProfile : Profile
{
    public ProductFeatureMappingProfile()
    {
        this.CreateMap<Entities.ProductFeature, ShowFeatureInCompareViewModel>();
    }
}