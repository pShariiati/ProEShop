using AutoMapper;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Mappings;

public class ReturnProductItemMappingProfile : Profile
{
    public ReturnProductItemMappingProfile()
    {
        this.CreateMap<Entities.ReturnProductItem, ReturnProductItemViewModel>()
            .ForMember(dest => dest.PersianTitle,
                options =>
                    options.MapFrom(src => src.ProductVariant.Product.PersianTitle))
            .ForMember(dest => dest.ProductImage,
                options =>
                    options.MapFrom(src => src.ProductVariant.Product.ProductMedia.First().FileName))
            .ForMember(dest => dest.ShopName,
                options =>
                    options.MapFrom(src => src.ProductVariant.Seller.ShopName))
            .ForMember(dest => dest.GuaranteeTitle,
                options =>
                    options.MapFrom(src => src.ProductVariant.Guarantee.Title))
            .ForMember(dest => dest.VariantValue,
                options =>
                    options.MapFrom(src => src.ProductVariant.Variant.Value))
            .ForMember(dest => dest.VariantColorCode,
                options =>
                    options.MapFrom(src => src.ProductVariant.Variant.ColorCode))
            .ForMember(dest => dest.IsVariantColor,
                options =>
                    options.MapFrom(src => src.ProductVariant.Variant.IsColor));
    }
}