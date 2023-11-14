using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicationAPI.Models;

[Table("unit_dose_orders")]
public partial class UnitDoseOrder
{
    [Key]
    [Column("unit_dose_order_id")]
    public int UnitDoseOrderId { get; set; }

    [Column("patient_id")]
    public int PatientId { get; set; }

    [Column("medication_id")]
    public int MedicationId { get; set; }

    [Column("dosage")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Dosage { get; set; }

    [Column("sig")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sig { get; set; }

    [Column("dosage_route")]
    [StringLength(10)]
    [Unicode(false)]
    public string? DosageRoute { get; set; }

    [Column("pharmacist_initials")]
    [StringLength(3)]
    [Unicode(false)]
    public string PharmacistInitials { get; set; } = null!;

    [Column("entered_date", TypeName = "date")]
    public DateTime? EnteredDate { get; set; }

    [ForeignKey("MedicationId")]
    [InverseProperty("UnitDoseOrders")]
    public virtual Medication Medication { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("UnitDoseOrders")]
    public virtual Patient Patient { get; set; } = null!;
}
