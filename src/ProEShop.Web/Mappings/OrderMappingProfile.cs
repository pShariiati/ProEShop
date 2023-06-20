using AutoMapper;
using ProEShop.Common.Helpers;
using ProEShop.Entities.Enums;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        #region Parameters

        DateTime now = default;

        #endregion

        this.CreateMap<Entities.Order, ShowOrderViewModel>()
            .ForMember(dest => dest.CreatedDateTime,
                options =>
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDateTime()))
            .ForMember(dest => dest.Destination,
                options =>
                    options.MapFrom(src => src.Address.Province.Title + " - " + src.Address.City.Title));

        this.CreateMap<Entities.Order, ShowOrderInProfileViewModel>()
            .ForMember(dest => dest.CreatedDateTime,
                options =>
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDate()))
            .ForMember(dest => dest.CanReturnProduct,
                options =>
                    options.MapFrom(src => !src.LastDeliveredParcelPostToClientDateTime.HasValue || src.LastDeliveredParcelPostToClientDateTime.Value.AddDays(7) > now))
            .ForMember(dest => dest.ProductImages,
                options =>
                    options.MapFrom(src => src.ParcelPostItems.OrderBy(x => x.ParcelPostId).Take(7).Select(x => x.ProductVariant.Product.ProductMedia.First().FileName)));

        this.CreateMap<Entities.ParcelPost, ShowParcelPostInDeliveryOrdersViewModel>();

        this.CreateMap<Entities.Order, ShowOrderInDeliveryOrdersViewModel>()
            .ForMember(dest => dest.CreatedDateTime,
                options =>
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDateTime()))
            .ForMember(dest => dest.Destination,
                options =>
                    options.MapFrom(src => src.Address.Province.Title + " - " + src.Address.City.Title))
            .ForMember(dest => dest.DeliveredParcelPostsToPostCount,
                options =>
                    options.MapFrom(src => src.ParcelPosts.Count(x =>
                        x.Status == ParcelPostStatus.DeliveredToClient || x.Status == ParcelPostStatus.DeliveredToPost)));

        this.CreateMap<Entities.Order, OrderDetailsViewModel>()
            .ForMember(dest => dest.CreatedDateTime,
                options =>
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDateTime()));

        this.CreateMap<Entities.ParcelPost, ParcelPostForOrderDetailsViewModel>();

        this.CreateMap<Entities.ParcelPostItem, ParcelPostItemForOrderDetailsViewModel>()
            .ForMember(dest => dest.ProductPicture,
                options =>
                    options.MapFrom(src => src.ProductVariant.Product.ProductMedia.First().FileName));

        this.CreateMap<Entities.Order, VerifyPageDataViewModel>();

        this.CreateMap<Entities.Order, ReturnProductViewModel>();

        this.CreateMap<Entities.ParcelPostItem, ParcelPostItemInReturnProduct>()
            .ForMember(dest => dest.ProductPicture,
                options =>
                    options.MapFrom(src => src.ProductVariant.Product.ProductMedia.First().FileName));
    }
}