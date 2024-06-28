using AutoMapper;
using ProEShop.ViewModels.UserHistories;

namespace ProEShop.Web.Mappings;

public class UserHistoryMappingProfile : Profile
{
    public UserHistoryMappingProfile()
    {
        #region Parameters

        long userId = 0;

        #endregion

        this.CreateMap<Entities.UserHistory, ShowUserHistoryViewModel>()
            .ForMember(dest => dest.Price,
                options =>
                    options.MapFrom(src =>
                        src.Product.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().Price
                    ))
            .ForMember(dest => dest.FinalPrice,
                options =>
                    options.MapFrom(src =>
                        src.Product.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().FinalPrice
                    ))
            .ForMember(dest => dest.OffPercentage,
                options =>
                    options.MapFrom(src =>
                        src.Product.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First().OffPercentage
                    ))
            .ForMember(dest => dest.CountInCart,
                options =>
                    options.MapFrom(src =>
                        src.Product.ProductVariants.OrderBy(x => x.OffPrice ?? x.Price).First()
                            .Carts
                            .Where(c => c.UserId == userId)
                            .Select(u => u.Count)
                            .SingleOrDefault()
                    ))
            .ForMember(dest => dest.ProductImage,
                options =>
                    options.MapFrom(src => src.Product.ProductMedia.First().FileName));
    }
}