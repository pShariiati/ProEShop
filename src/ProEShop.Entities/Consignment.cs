using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("Consignments")]
public class Consignment : EntityBase, IAuditableEntity
{
    #region Properties

    public long SellerId { get; set; }

    public DateTime DeliveryDate { get; set; }

    [Column(TypeName = "ntext")]
    public string Description { get; set; }

    public ConsignmentStatus ConsignmentStatus { get; set; }

    #endregion

    #region Relations

    public Seller Seller { get; set; }

    public ICollection<ConsignmentItem> ConsignmentItems { get; set; }
        = new List<ConsignmentItem>();

    #endregion
}

public enum ConsignmentStatus : byte
{
    [Display(Name = "در انتظار تایید")]
    AwaitingApproval,

    [Display(Name = "تایید شده و در انتظار ارسال محموله")]
    ConfirmAndAwaitingForConsignment,

    [Display(Name = "دریافت شده")]
    Received,

    [Display(Name = "رد شده")]
    Rejected,

    [Display(Name = "لغو شده")]
    Canceled
}