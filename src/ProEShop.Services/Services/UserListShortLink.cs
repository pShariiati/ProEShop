using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Services.Services;

public class UserListShortLinkService : GenericService<UserListShortLink>, IUserListShortLinkService
{
    private readonly DbSet<UserListShortLink> _userListShortLinks;
    private readonly IMapper _mapper;

    public UserListShortLinkService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _userListShortLinks = uow.Set<UserListShortLink>();
    }

    public Task<UserListShortLink> GetUserListShortLinkForCreateUserList()
    {
        return _userListShortLinks
            .Where(x => x.IsUsed == false)
            .OrderBy(x => Guid.NewGuid())
            .FirstAsync();
    }
}
