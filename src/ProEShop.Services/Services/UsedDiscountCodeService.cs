using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class UsedDiscountCodeService : CustomGenericService<UsedDiscountCode>, IUsedDiscountCodeService
{
    private readonly DbSet<UsedDiscountCode> _usedDiscountCodes;
    private readonly IMapper _mapper;

    public UsedDiscountCodeService(
        IUnitOfWork uow,
        IMapper mapper) : base(uow)
    {
        _mapper = mapper;
        _usedDiscountCodes = uow.Set<UsedDiscountCode>();
    }

    public Task<long> GetCountOfUsedDiscount(long discountCodeId)
    {
        return _usedDiscountCodes.LongCountAsync(x => x.DiscountCodeId == discountCodeId);
    }
}