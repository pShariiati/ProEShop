using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.Services.Contracts.Identity;

namespace ProEShop.Services.Services;

public class UsedDiscountCodeService : CustomGenericService<UsedDiscountCode>, IUsedDiscountCodeService
{
    private readonly DbSet<UsedDiscountCode> _usedDiscountCodes;
    private readonly IMapper _mapper;
    private readonly IApplicationUserManager _userManager;

    public UsedDiscountCodeService(
        IUnitOfWork uow,
        IMapper mapper,
        IApplicationUserManager userManager) : base(uow)
    {
        _mapper = mapper;
        _userManager = userManager;
        _usedDiscountCodes = uow.Set<UsedDiscountCode>();
    }

    public Task<long> GetCountOfUsedDiscount(long discountCodeId)
    {
        return _usedDiscountCodes.LongCountAsync(x => x.DiscountCodeId == discountCodeId);
    }

    public Task<long> GetCountOfUsedDiscountByOneUser(long discountCodeId)
    {
        var userId = _userManager.GetLoggedInUser();
        return _usedDiscountCodes
            .Where(x => x.UserId == userId)
            .LongCountAsync(x => x.DiscountCodeId == discountCodeId);
    }
}