using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalMVC.Models;

[Table("patients")]
public partial class Patient
{
    [Key]
    [Column("patient_id")]
    public int PatientId { get; set; }

    [Column("first_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Column("gender")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Gender { get; set; }

    [Column("birth_date", TypeName = "date")]
    public DateTime? BirthDate { get; set; }

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

    [Column("health_card_num")]
    public long? HealthCardNum { get; set; }

    [Column("allergies")]
    [StringLength(80)]
    [Unicode(false)]
    public string? Allergies { get; set; }

    [Column("patient_height", TypeName = "decimal(3, 0)")]
    public decimal? PatientHeight { get; set; }

    [Column("patient_weight", TypeName = "decimal(4, 0)")]
    public decimal? PatientWeight { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<Admission> Admissions { get; } = new List<Admission>();

    [InverseProperty("Patient")]
    public virtual ICollection<Encounter> Encounters { get; } = new List<Encounter>();

    [ForeignKey("ProvinceId")]
    [InverseProperty("Patients")]
    public virtual Province? Province { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<UnitDoseOrder> UnitDoseOrders { get; } = new List<UnitDoseOrder>();
}
