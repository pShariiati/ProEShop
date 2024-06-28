using ProEShop.Common.IdentityToolkit;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Web.Pages.Profile.Orders;

public class DetailsModel : ProfilePageBase
{
    #region Constructor

    private readonly IOrderService _orderService;

    public DetailsModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    #endregion

    public OrderDetailsViewModel OrderDetails { get; set; }

    public async Task OnGet(long orderNumber)
    {
        var userId = User.Identity.GetLoggedInUserId();
        OrderDetails = await _orderService.GetOrderDetailsInProfile(orderNumber, userId);
    }
}