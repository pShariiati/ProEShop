using ProEShop.Entities;
using ProEShop.ViewModels.UserLists;

namespace ProEShop.Services.Contracts;

public interface IUserListService : IGenericService<UserList>
{
    /// <summary>
    /// نمایش لیست های کاربر
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<UserListItemForProductInfoViewModel>> GetUserListInProductInfo(long productId, long userId);

    /// <summary>
    /// تمامی لیست های کاربر
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<long>> GetAllUserListIds(long userId);

    /// <summary>
    /// چند تا لیست رو تیک زده ؟ برای مثال دو تا
    /// این دو رکورد رو در داخل تمامی لیست های کاربر سرچ میکنیم
    /// اگه دو رکورد پیدا شد همه چی اوکیه
    /// </summary>
    /// <param name="userListIds"></param>
    /// <param name="allUserListIds"></param>
    /// <returns></returns>
    bool CheckUserListIdsForUpdate(List<long> userListIds, List<long> allUserListIds);

    /// <summary>
    /// بررسی تکراری بودن لیست کاربر
    /// آیا این کاربر از قبل همچین لیستی با این عنوان داشته است ؟
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    Task<bool> CheckForTitleDuplicate(long userId, string title);
}