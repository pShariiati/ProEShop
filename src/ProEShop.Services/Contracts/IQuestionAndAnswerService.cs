using ProEShop.Entities;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.QuestionsAndAnswers;

namespace ProEShop.Services.Contracts;

public interface IQuestionAndAnswerService : IGenericService<ProductQuestionAndAnswer>
{
    /// <summary>
    /// گرفتن سوالات محصولات به صورت صفحه بندی شده
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="pageNumber"></param>
    /// <param name="sortBy"></param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    Task<List<ProductQuestionForProductInfoViewModel>> GetQuestionsByPagination(long productId, int pageNumber, QuestionsSortingForProductInfo sortBy, SortingOrder orderBy);

    /// <summary>
    /// بررسی وجود داشتن این جواب و اینکه حتما باید جواب باشد
    /// یعنی پرنتش نال نباشد
    /// </summary>
    /// <param name="answerId"></param>
    /// <returns></returns>
    Task<bool> IsExistsAndAnswer(long answerId);
}