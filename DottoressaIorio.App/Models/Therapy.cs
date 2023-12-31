using System.ComponentModel.DataAnnotations;

namespace DottoressaIorio.App.Models
{
    public class Therapy : IDataHandler
    {
        public int TherapyId { get; set; }
        [Required(ErrorMessage = "La descrizione � richiesta.")]
        public string Description { get; set; }
        public int PatientId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditDate { get; set; }
        public bool Deleted { get; set; }
        public Patient Patient { get; set; }
    }
}
