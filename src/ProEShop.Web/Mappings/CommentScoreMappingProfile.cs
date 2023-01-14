using AutoMapper;
using ProEShop.Common.Helpers;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class CommentScoreMappingProfile : Profile
{
    public CommentScoreMappingProfile()
    {
        this.CreateMap<Entities.CommentScore, LikedCommentByUserViewModel>();
    }
}