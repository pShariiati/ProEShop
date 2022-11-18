using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.Common.Helpers;
public static class CommonHelpers
{
    public static bool IsNumericType(this string input)
    {
        switch (input)
        {
            case "Int32":
            case "Int64":
                return true;
            default:
                return false;
        }
    }
}
