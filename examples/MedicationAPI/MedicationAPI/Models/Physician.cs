using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicationAPI.Models;

[Table("physicians")]
public partial class Physician
{
    [Key]
    [Column("physician_id")]
    public int PhysicianId { get; set; }

    [Column("first_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Column("specialty")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Specialty { get; set; }

    [Column("phone")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("ohip_registration")]
    public int? OhipRegistration { get; set; }

    [InverseProperty("AttendingPhysician")]
    public virtual ICollection<Admission> Admissions { get; } = new List<Admission>();

    [InverseProperty("Physician")]
    public virtual ICollection<Encounter> Encounters { get; } = new List<Encounter>();
}
