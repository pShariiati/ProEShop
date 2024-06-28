using ProEShop.Entities;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Contracts;

public interface IAnswerScoreService : ICustomGenericService<ProductQuestionAnswerScore>
{
    /// <summary>
    /// از داخل این جواب ها کدام یک توسط این کاربر لایک و دیس لایک شده است ؟
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="answerIds"></param>
    /// <returns></returns>
    Task<List<LikedAnswerByUserViewModel>> GetLikedAnswersLikedByUser(long userId, long[] answerIds);
}