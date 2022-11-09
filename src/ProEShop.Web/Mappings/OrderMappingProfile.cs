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
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDateTime()))
            .ForMember(dest => dest.Destination,
                options =>
                    options.MapFrom(src => src.Address.Province.Title + " - " + src.Address.City.Title));

        this.CreateMap<Entities.Order, OrderDetailsViewModel>()
            .ForMember(dest => dest.CreatedDateTime,
                options =>
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDateTime()));

        this.CreateMap<Entities.ParcelPost, ParcelPostForOrderDetailsViewModel>();

        this.CreateMap<Entities.ParcelPostItem, ParcelPostItemForOrderDetailsViewModel>()
            .ForMember(dest => dest.ProductPicture,
                options =>
                    options.MapFrom(src => src.ProductVariant.Product.ProductMedia.First().FileName));
    }
}