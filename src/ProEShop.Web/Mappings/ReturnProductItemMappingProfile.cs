using AutoMapper;
using ProEShop.ViewModels.Addresses;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Orders;
using ProEShop.ViewModels.Products;

namespace ProEShop.Web.Mappings;

public class ReturnProductItemMappingProfile : Profile
{
    public ReturnProductItemMappingProfile()
    {
        this.CreateMap<Entities.ReturnProductItem, ReturnProductItemViewModel>();
    }
}