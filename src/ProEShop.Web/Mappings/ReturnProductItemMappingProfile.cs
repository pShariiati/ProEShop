using AutoMapper;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Mappings;

public class ReturnProductItemMappingProfile : Profile
{
    public ReturnProductItemMappingProfile()
    {
        this.CreateMap<Entities.ReturnProductItem, ReturnProductItemViewModel>();
    }
}