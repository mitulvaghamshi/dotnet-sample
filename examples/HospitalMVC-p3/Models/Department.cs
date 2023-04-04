using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalMVC.Models;

[Table("departments")]
public partial class Department
{
    [Key]
    [Column("department_id")]
    public int DepartmentId { get; set; }

    [Column("department_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string DepartmentName { get; set; } = null!;

    [Column("manager_first_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ManagerFirstName { get; set; }

    [Column("manager_last_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ManagerLastName { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; } = new List<PurchaseOrder>();
}
