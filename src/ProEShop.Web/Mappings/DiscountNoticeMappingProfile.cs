using AutoMapper;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.DiscountNotices;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class DiscountNoticeMappingProfile : Profile
{
    public DiscountNoticeMappingProfile()
    {
        this.CreateMap<Entities.DiscountNotice, AddDiscountNoticeViewModel>()
            .ReverseMap();
    }
}