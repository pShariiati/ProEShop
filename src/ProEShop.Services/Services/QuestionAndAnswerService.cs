using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.QuestionsAndAnswers;

namespace ProEShop.Services.Services;

public class QuestionAndAnswerService : GenericService<ProductQuestionAndAnswer>, IQuestionAndAnswerService
{
    private readonly DbSet<ProductQuestionAndAnswer> _questionsAndAnswers;
    private readonly IMapper _mapper;

    public QuestionAndAnswerService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _questionsAndAnswers = uow.Set<ProductQuestionAndAnswer>();
    }

    public async Task<List<ProductQuestionForProductInfoViewModel>> GetQuestionsByPagination(long productId, int pageNumber, QuestionsSortingForProductInfo sortBy, SortingOrder orderBy)
    {
        var query = _questionsAndAnswers
            .Where(x => x.ParentId == null)
            .Where(x => x.IsConfirmed)
            .Where(x => x.ProductId == productId);

        #region OrderBy

        if (sortBy == QuestionsSortingForProductInfo.MostUseful)
        {

        }
        else
        {
            query = query.CreateOrderByExpression(sortBy.ToString(), orderBy.ToString());
        }

        #endregion

        query = await GenericPaginationAsync(query, pageNumber, 2);

        return await _mapper.ProjectTo<ProductQuestionForProductInfoViewModel>(query)
            .ToListAsync();
    }

    public Task<bool> IsExistsAndAnswer(long answerId)
    {
        return _questionsAndAnswers
            .Where(x => x.Id == answerId)
            .AnyAsync(x => x.ParentId != null);
    }
}
