using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class WalletService : GenericService<Wallet>, IWalletService
{
    private readonly DbSet<Wallet> _wallets;
    private readonly IMapper _mapper;

    public WalletService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _wallets = uow.Set<Wallet>();
    }

    public Task<Wallet> FindByTrackingNumber(long trackingNumber, long userId)
    {
        return _wallets.Where(x => x.UserId == userId)
            .SingleOrDefaultAsync(x => x.TrackingNumber == trackingNumber);
    }
}
