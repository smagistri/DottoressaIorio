using System.ComponentModel.DataAnnotations;

namespace DottoressaIorio.App.Models;

public class TherapyTemplate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? EditDate { get; set; }
    public bool Deleted { get; set; }
}