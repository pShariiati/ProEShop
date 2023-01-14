using ProEShop.Entities;
using ProEShop.ViewModels.CategoryFeatures;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Contracts;

public interface ICommentScoreService : ICustomGenericService<CommentScore>
{
    /// <summary>
    /// از داخل این کامنت ها کدام یک توسط این کاربر لایک و دیس لایک شده است ؟
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="commentIds"></param>
    /// <returns></returns>
    Task<List<LikedCommentByUserViewModel>> GetLikedCommentsLikedByUser(long userId, long[] commentIds);
}