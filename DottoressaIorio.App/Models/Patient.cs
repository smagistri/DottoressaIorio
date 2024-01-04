using System.ComponentModel.DataAnnotations;

namespace DottoressaIorio.App.Models;

public class Patient
{
    public int PatientId { get; set; }
    public string? Title { get; set; }

    [Required(ErrorMessage = "First Name is required.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Date of Birth is required.")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public string? PlaceOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; }

    public List<Therapy> Therapies { get; set; }
}

