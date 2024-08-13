using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class KategorijePrihoda
{
    [Key]
    public int IDKategorijePrihoda { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public ICollection<Prihodi> Prihodi { get; set; }
}
