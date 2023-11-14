using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Doctors.Models;

public class Doctor
{
    [JsonPropertyName("physicianId")]
    public int PhysicianId { get; set; }

    [JsonPropertyName("firstName"), DisplayName("First Name")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName"), DisplayName("Last Name")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("specialty")]
    public string Specialty { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("ohipRegistration"), DisplayName("OHIP Registration")]
    public int OhipRegistration { get; set; }
}
