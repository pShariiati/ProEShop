using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class ProductShortLinkService : GenericService<ProductShortLink>, IProductShortLinkService
{
    private readonly DbSet<ProductShortLink> _productShortLinks;
    private readonly IMapper _mapper;

    public ProductShortLinkService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _productShortLinks = uow.Set<ProductShortLink>();
    }

    public Task<ProductShortLink> GetProductShortLinkForCreateProduct()
    {
        return _productShortLinks
            .OrderBy(x => Guid.NewGuid())
            .FirstAsync();
    }
}
