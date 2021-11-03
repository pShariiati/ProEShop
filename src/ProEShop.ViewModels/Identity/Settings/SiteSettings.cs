﻿using System;
using Microsoft.AspNetCore.Identity;
using ProEShop.ViewModels.Identity.Settings;

namespace ProEShop.ViewModels.Identity.Settings
{
    public class SiteSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public bool EnableEmailConfirmation { get; set; }
        public TimeSpan EmailConfirmationTokenProviderLifespan { get; set; }
        public PasswordOptions PasswordOptions { get; set; }
        public LockoutOptions LockoutOptions { get; set; }
        public CookieOptions CookieOptions { get; set; }
    }
}