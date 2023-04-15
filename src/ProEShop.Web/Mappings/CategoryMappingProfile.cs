using AutoMapper;
using ProEShop.Entities;
using ProEShop.ViewModels.Addresses;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.Search;

namespace ProEShop.Web.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        #region Parameters

        string brandSlug = null;

        #endregion

        this.CreateMap<Entities.Category, SearchOnCategoryViewModel>()
            .ForMember(dest => dest.Products,
                options =>
                    options.MapFrom(src => src.Products
                        .Where(x => brandSlug == null || x.Brand.Slug == brandSlug)
                        .OrderBy(x => x.Id)
                        .Take(2)))
            .ForMember(dest => dest.ProductsCount,
                options =>
                    options.MapFrom(src => src.Products.LongCount(x => brandSlug == null || x.Brand.Slug == brandSlug)));
        this.CreateMap<Entities.CategoryBrand, ShowBrandInSearchOnCategoryViewModel>();
        this.CreateMap<Entities.Product, ShowProductInSearchOnCategoryViewModel>()
            .ForMember(dest => dest.Picture,
                options =>
                    options.MapFrom(src => src.ProductMedia.First().FileName))
            .ForMember(dest => dest.Score,
                options =>
                    options.MapFrom(src =>
                        src.ProductComments.Any() ?
                            src.ProductComments.Average(pc => pc.Score)
                            : 0
                    ))
            .ForMember(dest => dest.FinalPrice,
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
            .ForMember(dest => dest.Price,
                options =>
                    options.MapFrom(src =>
                        src.ProductStockStatus == ProductStockStatus.Available
                            ?
                            src.ProductVariants.Any()
                                ?
                                src.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().Price
                                :
                                0
                            :
                            0
                    ))
            .ForMember(dest => dest.OffPercentage,
                options =>
                    options.MapFrom(src =>
                        src.ProductStockStatus == ProductStockStatus.Available
                            ?
                            src.ProductVariants.Any()
                                ?
                                src.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().OffPercentage
                                :
                                null
                            :
                            null
                    ))
            .ForMember(dest => dest.LastInventoryCount,
                options =>
                    options.MapFrom(src =>
                        src.ProductStockStatus == ProductStockStatus.Available
                            ?
                            src.ProductVariants.Any()
                                ?
                                    src.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().Count > 3
                                    ? (byte)0
                                    : (byte)src.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().Count
                                :
                                (byte)0
                            :
                            (byte)0
                    ));
    }
}