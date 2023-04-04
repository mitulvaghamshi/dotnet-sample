using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicationAPI.Models;

[Table("vendors")]
public partial class Vendor
{
    [Key]
    [Column("vendor_id")]
    public int VendorId { get; set; }

    [Column("vendor_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string VendorName { get; set; } = null!;

    [Column("street_address")]
    [StringLength(30)]
    [Unicode(false)]
    public string? StreetAddress { get; set; }

    [Column("city")]
    [StringLength(30)]
    [Unicode(false)]
    public string? City { get; set; }

    [Column("province_id")]
    [StringLength(2)]
    [Unicode(false)]
    public string? ProvinceId { get; set; }

    [Column("postal_code")]
    [StringLength(7)]
    [Unicode(false)]
    public string? PostalCode { get; set; }

    [Column("contact_first_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ContactFirstName { get; set; }

    [Column("contact_last_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ContactLastName { get; set; }

    [Column("purchases_ytd", TypeName = "decimal(9, 2)")]
    public decimal? PurchasesYtd { get; set; }

    [InverseProperty("PrimaryVendor")]
    public virtual ICollection<Item> Items { get; } = new List<Item>();

    [ForeignKey("ProvinceId")]
    [InverseProperty("Vendors")]
    public virtual Province? Province { get; set; }

    [InverseProperty("Vendor")]
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; } = new List<PurchaseOrder>();
}
