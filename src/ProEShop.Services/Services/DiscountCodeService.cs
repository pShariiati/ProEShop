using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.DiscountCodes;

namespace ProEShop.Services.Services;

public class DiscountCodeService : GenericService<DiscountCode>, IDiscountCodeService
{
    private readonly DbSet<DiscountCode> _discountCodes;
    private readonly IMapper _mapper;

    public DiscountCodeService(
        IUnitOfWork uow,
        IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _discountCodes = uow.Set<DiscountCode>();
    }

    public async Task<CheckDiscountCodeViewModel> CheckForDiscountPrice(string code)
    {
        var discountCode = await _discountCodes.SingleOrDefaultAsync(x => x.Code == code);

        if (discountCode is null)
        {
            return new(false, default);
        }

        return new(true, discountCode.Price);
    }
}