using AutoMapper;
using ProEShop.Common.Helpers;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class ProductQuestionAndAnswerMappingProfile : Profile
{
    public ProductQuestionAndAnswerMappingProfile()
    {
        this.CreateMap<Entities.ProductQuestionAndAnswer, ProductQuestionForProductInfoViewModel>();
        this.CreateMap<Entities.ProductQuestionAndAnswer, ProductQuestionAnswerForProductInfoViewModel>()
            .ForMember(dest => dest.Like,
                options =>
                    options.MapFrom(src => src.ProductsQuestionsAnswersScores.LongCount(x => x.IsLike)))
            .ForMember(dest => dest.Dislike,
                options =>
                    options.MapFrom(src => src.ProductsQuestionsAnswersScores.LongCount(x => !x.IsLike)))
            .ForMember(dest => dest.Name,
                options =>
                    options.MapFrom(src => src.IsUnknown ? null
                        : (
                            src.UserId != null ? src.User.FullName : src.Seller.ShopName
                        )))
            .ForMember(dest => dest.IsShop,
                options =>
                    options.MapFrom(src => src.SellerId != null));
    }
}