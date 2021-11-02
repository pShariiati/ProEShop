using Microsoft.AspNetCore.Identity;

namespace ProEShop.ViewModels.Identity.Settings;

public class SiteSettings
{
    public bool EnableEmailConfirmation { get; set; }
    public TimeSpan EmailConfirmationTokenProviderLifespan { get; set; }
    public PasswordOptions PasswordOptions { get; set; }
    public LockoutOptions LockoutOptions { get; set; }
    public CookieOptions CookieOptions { get; set; }
}