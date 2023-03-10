using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Pages.Profile.Orders;

public class IndexModel : PageModel
{
    #region Constructor

    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    #endregion

    public ShowOrdersInProfileViewModel Orders { get; set; }
        = new();

    public async Task OnGet()
    {
        Orders = await _orderService.GetOrdersInProfile(Orders);
    }

    /// <summary>
    /// نمایش محصولات به صورت صفحه بندی شده
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetShowOrdersByPagination(int pageNumber)
    {
        var orders = await _orderService.GetOrdersInProfile(pageNumber);
        return Partial("_Orders", orders);
    }
}