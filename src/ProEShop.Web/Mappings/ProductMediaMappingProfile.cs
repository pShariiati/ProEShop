using AutoMapper;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class ProductMediaMappingProfile : Profile
{
    public ProductMediaMappingProfile()
    {
        this.CreateMap<Entities.ProductMedia, ProductMediaForProductDetailsViewModel>();
        this.CreateMap<Entities.ProductMedia, ProductMediaForProductInfoViewModel>();
    }
}
