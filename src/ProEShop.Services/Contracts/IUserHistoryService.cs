using ProEShop.Entities;
using ProEShop.ViewModels.UserHistories;

namespace ProEShop.Services.Contracts;

public interface IUserHistoryService : ICustomGenericService<UserHistory>
{
    /// <summary>
    /// گرفتن محصولات برای صفحه بازدید های اخیر
    /// </summary>
    /// <returns></returns>
    Task<List<ShowUserHistoryViewModel>> GetUserHistories();
}