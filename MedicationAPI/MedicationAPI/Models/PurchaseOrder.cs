using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicationAPI.Models;

[Table("purchase_orders")]
public partial class PurchaseOrder
{
    [Key]
    [Column("purchase_order_id")]
    public int PurchaseOrderId { get; set; }

    [Column("order_date", TypeName = "date")]
    public DateTime OrderDate { get; set; }

    [Column("department_id")]
    public int? DepartmentId { get; set; }

    [Column("vendor_id")]
    public int? VendorId { get; set; }

    [Column("total_amount", TypeName = "decimal(9, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column("order_status")]
    [StringLength(10)]
    [Unicode(false)]
    public string? OrderStatus { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("PurchaseOrders")]
    public virtual Department? Department { get; set; }

    [InverseProperty("PurchaseOrder")]
    public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; } = new List<PurchaseOrderLine>();

    [ForeignKey("VendorId")]
    [InverseProperty("PurchaseOrders")]
    public virtual Vendor? Vendor { get; set; }
}
