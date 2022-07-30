using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class UserProductFavoriteService : CustomGenericService<UserProductFavorite>, IUserProductFavoriteService
{
    private readonly DbSet<UserProductFavorite> _userProductsFavorites;

    public UserProductFavoriteService(IUnitOfWork uow) : base(uow)
    {
        _userProductsFavorites = uow.Set<UserProductFavorite>();
    }
}