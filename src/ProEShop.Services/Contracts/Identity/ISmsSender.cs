namespace ProEShop.Services.Contracts.Identity;

public interface ISmsSender
{
    #region BaseClass

    Task<bool> SendSmsAsync(string number, string message);

    #endregion

    #region CustomMethods

    #endregion
}
