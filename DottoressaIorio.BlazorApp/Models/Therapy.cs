namespace DottoressaIorio.BlazorApp.Models;

public class Therapy
{
    public int TherapyId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; }

    // Foreign key to Patient
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
}