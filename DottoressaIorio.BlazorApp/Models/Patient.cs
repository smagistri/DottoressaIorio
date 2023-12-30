namespace DottoressaIorio.BlazorApp.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Navigation property for therapies
        public List<Therapy> Therapies { get; set; } = new List<Therapy>();
    }
}
