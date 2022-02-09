using AutoMapper;
using ProEShop.Common.Helpers;
using ProEShop.Entities.Identity;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //this.CreateMap<string, string>()
        //    .ConvertUsing(str => str != null ? str.Trim() : null);
        this.CreateMap<User, CreateSellerViewModel>();
        this.CreateMap<CreateSellerViewModel, Entities.Seller>()
            .AddTransform<string>(str => str != null ? str.Trim() : null);
        this.CreateMap<CreateSellerViewModel, Entities.Identity.User>()
            .AddTransform<string>(str => str != null ? str.Trim() : null)
            .ForMember(x => x.BirthDate,
                opt => opt.Ignore());
        this.CreateMap<Entities.Seller, ShowSellerViewModel>()
            .ForMember(dest => dest.FullName,
                options =>
                    options.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.ProvinceAndCity,
                options =>
                    options.MapFrom(src => $"{src.Province.Title} - {src.City.Title}"))
            .ForMember(dest => dest.CreatedDateTime,
                options =>
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDate()))
        ;
    }
}