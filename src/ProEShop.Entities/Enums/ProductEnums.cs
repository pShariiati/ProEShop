using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.Entities.Enums;
public enum ProductDimensions : byte
{
    [Display(Name = "عادی")]
    Normal,

    [Display(Name = "کالا های بزرگ و سنگین")]
    Heavy,

    [Display(Name = "کالا های فوق سنگین")]
    UltraHeavy
}
