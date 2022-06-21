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

    #endregion
}

public enum ConsignmentStatus : byte
{
    AwaitingApproval,
    ConfirmAndAwaitingForConsignment,
    Received,
    Rejected,
    Canceled
}