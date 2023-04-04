using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicationAPI.Models;

[Table("items")]
public partial class Item
{
    [Key]
    [Column("item_id")]
    public int ItemId { get; set; }

    [Column("item_description")]
    [StringLength(30)]
    [Unicode(false)]
    public string ItemDescription { get; set; } = null!;

    [Column("item_cost", TypeName = "decimal(9, 2)")]
    public decimal? ItemCost { get; set; }

    [Column("quantity_on_hand")]
    public int? QuantityOnHand { get; set; }

    [Column("usage_ytd")]
    public int? UsageYtd { get; set; }

    [Column("primary_vendor_id")]
    public int? PrimaryVendorId { get; set; }

    [Column("order_quantity")]
    public int? OrderQuantity { get; set; }

    [Column("order_point")]
    public int? OrderPoint { get; set; }

    [ForeignKey("PrimaryVendorId")]
    [InverseProperty("Items")]
    public virtual Vendor? PrimaryVendor { get; set; }

    [InverseProperty("Item")]
    public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; } = new List<PurchaseOrderLine>();
}
