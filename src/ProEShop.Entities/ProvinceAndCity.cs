using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("ProvincesAndCities")]
public class ProvinceAndCity : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    public long? ParentId { get; set; }

    #endregion

    #region Relations

    public ProvinceAndCity Parent { get; set; }

    public ICollection<Seller> SellerProvinces { get; set; }

    public ICollection<Seller> SellerCities { get; set; }

    public ICollection<Address> AddressProvinces { get; set; }

    public ICollection<Address> AddressCities { get; set; }

    #endregion
}
