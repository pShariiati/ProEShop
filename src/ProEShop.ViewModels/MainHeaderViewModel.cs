using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProEShop.ViewModels.Carts;

namespace ProEShop.ViewModels;

public class MainHeaderViewModel
{
    public int AllProductsCountInCart { get; set; }

    public List<ShowCartInDropDownViewModel> Carts { get; set; }
}
