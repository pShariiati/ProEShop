using AutoMapper;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class ParcelPostItemMappingProfile : Profile
{
    public ParcelPostItemMappingProfile()
    {
        this.CreateMap<IEnumerable<Entities.ParcelPostItem>, ShowProductInProfileCommentViewModel>()
            .ForMember(dest => dest.Picture,
                options =>
                    options.MapFrom(src => src.First().ProductVariant.Product.ProductMedia.First().FileName))
            .ForMember(dest => dest.Title,
            options =>
                options.MapFrom(src => src.First().ProductVariant.Product.PersianTitle));
    }
}