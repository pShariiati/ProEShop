using AutoMapper;
using ProEShop.Common.Helpers;
using ProEShop.ViewModels.ProductComments;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class ProductCommentMappingProfile : Profile
{
    public ProductCommentMappingProfile()
    {
        this.CreateMap<Entities.ProductComment, ProductCommentForProductInfoViewModel>()
            .ForMember(dest => dest.CreatedDateTime,
                options =>
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDate()))
            .ForMember(dest => dest.Like,
                options =>
                    options.MapFrom(src => src.CommentsScores.LongCount(x => x.IsLike)))
            .ForMember(dest => dest.Dislike,
                options =>
                    options.MapFrom(src => src.CommentsScores.LongCount(x => !x.IsLike)))
            .ForMember(dest => dest.Name,
                options =>
                    options.MapFrom(src => src.IsUnknown
                        ? null
                        : (
                            src.UserId != null ? src.User.FullName : src.Seller.ShopName
                        )))
            .ForMember(dest => dest.IsShop,
                options =>
                    options.MapFrom(src => src.SellerId != null));

        this.CreateMap<Entities.ProductComment, ShowProductCommentInProfile>()
            .ForMember(dest => dest.MainPicture,
                options =>
                    options.MapFrom(src => src.Product.ProductMedia.First().FileName))
            .ForMember(dest => dest.IsSeller,
                options =>
                    options.MapFrom(src => src.SellerId != null))
            .ForMember(dest => dest.Like,
                options =>
                    options.MapFrom(src => src.CommentsScores.LongCount(x => x.IsLike)));
    }
}