using AutoMapper;
using ProEShop.Common.Helpers;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        this.CreateMap<Entities.Order, ShowOrderViewModel>()
            .ForMember(dest => dest.CreatedDateTime,
                options =>
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDate()))
            .ForMember(dest => dest.Destination,
                options =>
                    options.MapFrom(src => src.Address.Province.Title + " - " + src.Address.City.Title));
    }
}