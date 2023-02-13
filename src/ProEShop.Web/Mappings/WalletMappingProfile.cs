using AutoMapper;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.Wallets;

namespace ProEShop.Web.Mappings;

public class WalletMappingProfile : Profile
{
    public WalletMappingProfile()
    {
        this.CreateMap<AddValueToWalletViewModel, Entities.Wallet>();
    }
}