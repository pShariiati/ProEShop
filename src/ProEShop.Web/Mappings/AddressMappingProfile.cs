using AutoMapper;
using ProEShop.ViewModels.Addresses;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        this.CreateMap<Entities.Address, AddressInCheckoutPageViewModel>();
        this.CreateMap<Entities.Address, ShowAddressInProfileViewModel>();
    }
}