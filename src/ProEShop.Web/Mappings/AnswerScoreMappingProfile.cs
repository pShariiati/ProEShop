using AutoMapper;
using ProEShop.Common.Helpers;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class AnswerScoreMappingProfile : Profile
{
    public AnswerScoreMappingProfile()
    {
        this.CreateMap<Entities.ProductQuestionAnswerScore, LikedAnswerByUserViewModel>();
    }
}