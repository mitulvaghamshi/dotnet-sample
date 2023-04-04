using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicationAPI.Models;

[PrimaryKey("PatientId", "PhysicianId", "EncounterDateTime")]
[Table("encounters")]
public partial class Encounter
{
    [Key]
    [Column("patient_id")]
    public int PatientId { get; set; }

    [Key]
    [Column("physician_id")]
    public int PhysicianId { get; set; }

    [Key]
    [Column("encounter_date_time", TypeName = "datetime")]
    public DateTime EncounterDateTime { get; set; }

    [Column("notes")]
    [StringLength(250)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("Encounters")]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey("PhysicianId")]
    [InverseProperty("Encounters")]
    public virtual Physician Physician { get; set; } = null!;
}
