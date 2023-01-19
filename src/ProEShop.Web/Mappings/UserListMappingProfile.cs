using AutoMapper;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.UserLists;

namespace ProEShop.Web.Mappings;

public class UserListMappingProfile : Profile
{
    public UserListMappingProfile()
    {
        #region Parameters

        long productId = 0;

        #endregion

        this.CreateMap<Entities.UserList, UserListItemForProductInfoViewModel>()
            .ForMember(dest => dest.IsChecked,
                options =>
                    options.MapFrom(src => src.UserListsProducts.Any(ulp => ulp.ProductId == productId)));

        this.CreateMap<AddUserListViewModel, Entities.UserList>()
            .AddTransform<string>(str => str != null ? str.Trim() : null);
    }
}