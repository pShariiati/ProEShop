using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.UserLists;

namespace ProEShop.Services.Services;

public class UserListService : GenericService<UserList>, IUserListService
{
    private readonly DbSet<UserList> _userLists;
    private readonly IMapper _mapper;

    public UserListService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _userLists = uow.Set<UserList>();
    }

    public Task<List<UserListItemForProductInfoViewModel>> GetUserListInProductInfo(long productId, long userId)
    {
        return _userLists.Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Id)
            .ProjectTo<UserListItemForProductInfoViewModel>
            (
                configuration: _mapper.ConfigurationProvider,
                parameters: new { productId = productId }
            )
            .ToListAsync();
    }

    public Task<List<long>> GetAllUserListIds(long userId)
    {
        return _userLists.Where(x => x.UserId == userId)
            .Select(x => x.Id)
            .ToListAsync();
    }

    public bool CheckUserListIdsForUpdate(List<long> userListIds, List<long> allUserListIds)
    {
        var userListIdsCountFromDatabase = allUserListIds.LongCount(userListIds.Contains);

        return userListIds.Count == userListIdsCountFromDatabase;
    }

    public async Task<bool> CheckForTitleDuplicate(long userId, string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return true;
        }

        return await _userLists.Where(x => x.UserId == userId)
            .AnyAsync(x => x.Title == title.Trim());
    }

    public override async Task<DuplicateColumns> AddAsync(UserList entity)
    {
        var result = new List<string>();
        if (await _userLists.Where(x => x.UserId == entity.UserId).AnyAsync(x => x.Title == entity.Title))
            result.Add(nameof(Category.Title));
        if (!result.Any())
            await base.AddAsync(entity);
        return new(!result.Any())
        {
            Columns = result
        };
    }
}
