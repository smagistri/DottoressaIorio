using System.ComponentModel.DataAnnotations;

namespace DottoressaIorio.App.Models
{
    public class Therapy
    {
        public int TherapyId { get; set; }
        [Required(ErrorMessage = "La descrizione é richiesta.")]
        public string Description { get; set; }
        public int PatientId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditDate { get; set; }
        public bool Deleted { get; set; }
        public Patient Patient { get; set; }
    }
}
