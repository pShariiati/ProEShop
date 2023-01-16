using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.CategoryFeatures;
using ProEShop.ViewModels.DiscountNotices;

namespace ProEShop.Services.Services;

public class DiscountNoticeService : CustomGenericService<DiscountNotice>, IDiscountNoticeService
{
    private readonly DbSet<DiscountNotice> _discountNotices;
    private readonly IMapper _mapper;

    public DiscountNoticeService(
        IUnitOfWork uow,
        IMapper mapper) : base(uow)
    {
        _mapper = mapper;
        _discountNotices = uow.Set<DiscountNotice>();
    }

    public Task<AddDiscountNoticeViewModel> GetDataForAddDiscountNotice(long productId, long userId)
    {
        return _mapper.ProjectTo<AddDiscountNoticeViewModel>(
            _discountNotices.Where(x => x.UserId == userId)
                .Where(x => x.ProductId == productId)
        ).SingleOrDefaultAsync();
    }
}