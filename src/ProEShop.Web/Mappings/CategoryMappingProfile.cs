using AutoMapper;
using ProEShop.Entities;
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
                        .OrderByDescending(x => x.VisitCount)
                        .Take(2)))
            .ForMember(dest => dest.CategoryFeatures,
                options =>
                    options.MapFrom(src => src.FeatureConstantValues.GroupBy(x => x.FeatureId)))
            .ForMember(dest => dest.MinimumPrice,
                options =>
                    options.MapFrom(src => src.Products.Min(p => p.Price)))
            .ForMember(dest => dest.MaximumPrice,
                options =>
                    options.MapFrom(src => src.Products.Max(p => p.Price)))
            .ForMember(dest => dest.CategoryVariants,
                options =>
                    options.MapFrom(src => src.CategoryVariants.Where(x => x.Variant.IsConfirmed)))
            .ForMember(dest => dest.ProductsCount,
                options =>
                    options.MapFrom(src => src.Products.LongCount(x => brandSlug == null || x.Brand.Slug == brandSlug)));
        this.CreateMap<Entities.CategoryBrand, ShowBrandInSearchOnCategoryViewModel>();
        this.CreateMap<IGrouping<long, Entities.FeatureConstantValue>, ShowFeatureInSearchOnCategoryViewModel>()
            .ForMember(dest => dest.FeatureTitle,
                options =>
                    options.MapFrom(src => src.First().Feature.Title))
            .ForMember(dest => dest.FeatureId,
                options =>
                    options.MapFrom(src => src.First().FeatureId))
            .ForMember(dest => dest.Values,
                options =>
                    options.MapFrom(src => src.Select(x => x.Value)));
        this.CreateMap<Entities.CategoryVariant, ShowVariantInSearchOnCategoryViewModel>();
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
            .ForMember(dest => dest.ColorCodes,
                options =>
                    options.MapFrom(src => src.Category.IsVariantColor == true
                    ? src.ProductVariants
                        .Where(x => x.Count > 0)
                        .GroupBy(x => x.Variant.ColorCode)
                        .OrderBy(x => x.Key)
                        .Take(3)
                        .Select(x => x.Key)
                    : null))
            .ForMember(dest => dest.IsMoreThanThreeColors,
                options =>
                    options.MapFrom(src => src.Category.IsVariantColor == true && src.ProductVariants
                        .Where(x => x.Count > 0)
                        .GroupBy(x => x.Variant.ColorCode)
                        .Count() > 3))
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