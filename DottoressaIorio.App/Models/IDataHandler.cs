namespace DottoressaIorio.App.Models;

public interface IDataHandler
{
    public DateTime CreatedDate { get; set; }
    public DateTime? EditDate { get; set; }
    public bool Deleted { get; set; }
}