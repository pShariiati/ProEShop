using AutoMapper;
using ProEShop.ViewModels.DiscountNotices;

namespace ProEShop.Web.Mappings;

public class DiscountNoticeMappingProfile : Profile
{
    public DiscountNoticeMappingProfile()
    {
        this.CreateMap<Entities.DiscountNotice, AddDiscountNoticeViewModel>()
            .ReverseMap();
    }
}