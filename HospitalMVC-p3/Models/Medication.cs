using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalMVC.Models;

[Table("medications")]
public partial class Medication
{
    [Key]
    [Column("medication_id")]
    public int MedicationId { get; set; }

    [DisplayName("Description")]
    [Column("medication_description")]
    [StringLength(25)]
    [Unicode(false)]
    public string MedicationDescription { get; set; } = null!;

    [DisplayName("Cost")]
    [DataType(DataType.Currency)]
    [Column("medication_cost", TypeName = "decimal(9, 2)")]
    public decimal? MedicationCost { get; set; }

    [DisplayName("Package Size")]
    [Column("package_size")]
    [StringLength(20)]
    [Unicode(false)]
    public string? PackageSize { get; set; }

    [Column("strength")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Strength { get; set; }

    [Column("sig")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sig { get; set; }

    [DisplayName("Units Used YTD")]
    [Column("units_used_ytd")]
    public int? UnitsUsedYtd { get; set; }

    [DisplayName("Last Prescribed")]
    [DataType(DataType.Date)]
    [Column("last_prescribed_date", TypeName = "date")]
    public DateTime? LastPrescribedDate { get; set; }

    [InverseProperty("Medication")]
    public virtual ICollection<UnitDoseOrder> UnitDoseOrders { get; } = new List<UnitDoseOrder>();
}
