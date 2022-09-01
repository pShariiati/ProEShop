using AutoMapper;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        this.CreateMap<Entities.Cart, ProductVariantInCartForProductInfoViewModel>();
    }
}
