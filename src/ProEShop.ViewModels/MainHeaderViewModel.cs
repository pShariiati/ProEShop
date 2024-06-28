using ProEShop.ViewModels.Carts;

namespace ProEShop.ViewModels;

public class MainHeaderViewModel
{
    public int AllProductsCountInCart { get; set; }

    public List<ShowCartInDropDownViewModel> Carts { get; set; }
}
