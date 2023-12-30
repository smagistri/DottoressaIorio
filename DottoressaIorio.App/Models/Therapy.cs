namespace DottoressaIorio.App.Models
{
    // Patient.cs

    // Therapy.cs
    public class Therapy
    {
        public int TherapyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        // ... other properties

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }

}
