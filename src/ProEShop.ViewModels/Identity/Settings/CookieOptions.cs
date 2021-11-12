using System;

namespace ProEShop.ViewModels.Identity.Settings;

public class CookieOptions
{
    public string? AccessDeniedPath { get; set; }
    public string? CookieName { get; set; }
    public TimeSpan ExpireTimeSpan { get; set; }
    public string? LoginPath { get; set; }
    public string? LogoutPath { get; set; }
    public bool SlidingExpiration { get; set; }
}
