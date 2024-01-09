using System.ComponentModel.DataAnnotations;

namespace DottoressaIorio.App.Models;

public class TherapyTemplate : IDataHandler
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Il titolo � richiesto.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "La descrizione � richiesta.")]
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? EditDate { get; set; }
    public bool Deleted { get; set; }
}