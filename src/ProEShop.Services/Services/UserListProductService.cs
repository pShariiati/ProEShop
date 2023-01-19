using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Services.Services;

public class UserListProductService : CustomGenericService<UserListProduct>, IUserListProductService
{
    private readonly DbSet<UserListProduct> _userListProducts;
    private readonly IMapper _mapper;

    public UserListProductService(
        IUnitOfWork uow,
        IMapper mapper) : base(uow)
    {
        _mapper = mapper;
        _userListProducts = uow.Set<UserListProduct>();
    }

    public Task<List<UserListProduct>> GetUserListProducts(long productId, List<long> userListIds)
    {
        return _userListProducts.Where(x => x.ProductId == productId)
            .Where(x => userListIds.Contains(x.UserListId))
            .ToListAsync();
    }
}