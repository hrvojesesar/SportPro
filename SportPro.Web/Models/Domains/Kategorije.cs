using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class Kategorije
{
    [Key]
    public int IDKategorija { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public ICollection<Artikli> Artikli { get; set; }
}
