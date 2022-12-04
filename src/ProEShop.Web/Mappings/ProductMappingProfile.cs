using AutoMapper;
using ProEShop.Entities;
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
                    ))
            .ForMember(dest => dest.Price,
                options =>
                    options.MapFrom(src =>
                        src.ProductStockStatus == ProductStockStatus.Available
                            ?
                            src.ProductVariants.Any()
                                ?
                                src.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().FinalPrice
                                :
                                0
                            :
                            0
                    ));

        this.CreateMap<Entities.Product, ProductItemForShowProductInComparePartialViewModel>()
            .ForMember(dest => dest.MainPicture,
                options =>
                    options.MapFrom(src => src.ProductMedia.First().FileName))
            .ForMember(dest => dest.Score,
                options =>
                    options.MapFrom(src =>
                        src.ProductComments.Any() ?
                            src.ProductComments.Average(pc => pc.Score)
                            : 0
                    ))
            .ForMember(dest => dest.Price,
                options =>
                    options.MapFrom(src =>
                        src.ProductStockStatus == ProductStockStatus.Available
                            ?
                            src.ProductVariants.Any()
                                ?
                                src.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().FinalPrice
                                :
                                0
                            :
                            0
                    ))
            .ForMember(dest => dest.Count,
                options =>
                    options.MapFrom(src =>
                        src.ProductStockStatus == ProductStockStatus.Available
                            ?
                            src.ProductVariants.Any()
                                ?
                                    src.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().Count > 3
                                    ? (byte)0
                                    :
                                    (byte)src.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().Count
                                :
                                (byte)0
                            :
                            (byte)0
                    ));
    }
}