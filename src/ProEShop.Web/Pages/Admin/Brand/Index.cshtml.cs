using AutoMapper;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Web.Pages.Admin.Brand;

public class IndexModel : PageBase
{
    #region Constructor
    
    #endregion

    [BindProperty]
    public ShowBrandsViewModel Brands { get; set; }
        = new();

    public void OnGet()
    {
    }
}