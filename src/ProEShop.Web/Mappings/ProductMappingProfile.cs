using AutoMapper;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        this.CreateMap<Entities.Product, ShowProductInCompareViewModel>()
            .ForMember(dest => dest.MainPicture,
                options =>
                    options.MapFrom(src => src.ProductMedia.First().FileName))
            .ForMember(dest => dest.Score,
                options =>
                    options.MapFrom(src =>
                        src.ProductComments.Any() ?
                            src.ProductComments.Average(pc => pc.Score)
                            : 0
                    ));
    }
}