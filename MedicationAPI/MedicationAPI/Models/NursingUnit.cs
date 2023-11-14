using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicationAPI.Models;

[Table("nursing_units")]
public partial class NursingUnit
{
    [Key]
    [Column("nursing_unit_id")]
    [StringLength(10)]
    [Unicode(false)]
    public string NursingUnitId { get; set; } = null!;

    [Column("specialty")]
    [StringLength(20)]
    [Unicode(false)]
    public string Specialty { get; set; } = null!;

    [Column("manager_first_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ManagerFirstName { get; set; }

    [Column("manager_last_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ManagerLastName { get; set; }

    [Column("beds")]
    public int? Beds { get; set; }

    [Column("extension")]
    public int? Extension { get; set; }

    [InverseProperty("NursingUnit")]
    public virtual ICollection<Admission> Admissions { get; } = new List<Admission>();
}
