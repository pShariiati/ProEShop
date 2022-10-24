using NuGet.Configuration;
using Parbad.Gateway.Mellat;
using Parbad.Gateway.ZarinPal;
using Parbad.GatewayBuilders;

namespace ProEShop.Web.Helpers;

/// <summary>
/// اکانت درگاه های بانکی برای پرباد
/// </summary>
public class ParbadGatewaysAccounts :
    IGatewayAccountSource<MellatGatewayAccount>,
    IGatewayAccountSource<ZarinPalGatewayAccount>
{
    /// <summary>
    /// زرین پال
    /// </summary>
    /// <param name="accounts"></param>
    /// <returns></returns>
    public Task AddAccountsAsync(IGatewayAccountCollection<ZarinPalGatewayAccount> accounts)
    {
        accounts.Add(new ZarinPalGatewayAccount()
        {
            IsSandbox = true,
            MerchantId = "test"
        });
        return Task.CompletedTask; ;
    }

    /// <summary>
    /// بانک ملت
    /// </summary>
    /// <param name="accounts"></param>
    /// <returns></returns>
    public Task AddAccountsAsync(IGatewayAccountCollection<MellatGatewayAccount> accounts)
    {
        accounts.Add(new MellatGatewayAccount
        {
            TerminalId = 123,
            UserName = "test username",
            UserPassword = "12345678"
        });
        return Task.CompletedTask;
    }
}
