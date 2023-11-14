namespace PhysicianAPI.Models;

public partial class Physician
{
    public int PhysicianId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Specialty { get; set; }

    public string? Phone { get; set; }

    public int? OhipRegistration { get; set; }
}
