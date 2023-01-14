using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.CategoryFeatures;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Services;

public class AnswerScoreService : CustomGenericService<ProductQuestionAnswerScore>, IAnswerScoreService
{
    private readonly DbSet<ProductQuestionAnswerScore> _questionsAnswerScores;
    private readonly IMapper _mapper;

    public AnswerScoreService(
        IUnitOfWork uow,
        IMapper mapper) : base(uow)
    {
        _mapper = mapper;
        _questionsAnswerScores = uow.Set<ProductQuestionAnswerScore>();
    }

    public Task<List<LikedAnswerByUserViewModel>> GetLikedAnswersLikedByUser(long userId, long[] commentIds)
    {
        return _mapper.ProjectTo<LikedAnswerByUserViewModel>(
            _questionsAnswerScores
                .Where(x => x.UserId == userId)
                .Where(x => commentIds.Contains(x.AnswerId))
        ).ToListAsync();
    }
}