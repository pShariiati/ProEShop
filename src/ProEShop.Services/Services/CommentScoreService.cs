using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Services;

public class CommentScoreService : CustomGenericService<CommentScore>, ICommentScoreService
{
    private readonly DbSet<CommentScore> _commentScores;
    private readonly IMapper _mapper;

    public CommentScoreService(
        IUnitOfWork uow,
        IMapper mapper) : base(uow)
    {
        _mapper = mapper;
        _commentScores = uow.Set<CommentScore>();
    }

    public Task<List<LikedCommentByUserViewModel>> GetLikedCommentsLikedByUser(long userId, long[] commentIds)
    {
        return _mapper.ProjectTo<LikedCommentByUserViewModel>(
            _commentScores
                .Where(x => x.UserId == userId)
                .Where(x => commentIds.Contains(x.ProductCommentId))
        ).ToListAsync();
    }
}