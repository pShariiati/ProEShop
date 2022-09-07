using AutoMapper;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        #region Parameters

        DateTime now = default;

        #endregion

        this.CreateMap<Entities.Cart, ProductVariantInCartForProductInfoViewModel>();
        this.CreateMap<Entities.Cart, ShowCartInDropDownViewModel>()
            .ForMember(dest => dest.IsDiscountActive,
                options =>
                    options.MapFrom(src =>
                        src.ProductVariant.OffPercentage != null &&
                        (src.ProductVariant.StartDateTime <= now && src.ProductVariant.EndDateTime >= now)
                    ))
            .ForMember(dest => dest.ProductPicture,
                options =>
                    options.MapFrom(src => src.ProductVariant.Product.ProductMedia.First().FileName));
    }
}
