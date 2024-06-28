using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class ParcelPostService : GenericService<ParcelPost>, IParcelPostService
{
    private readonly DbSet<ParcelPost> _parcelPosts;
    private readonly IMapper _mapper;

    public ParcelPostService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _parcelPosts = uow.Set<ParcelPost>();
    }
}
