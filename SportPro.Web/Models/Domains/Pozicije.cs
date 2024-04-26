using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;
public class Pozicije
{
    [Key]
    public int IDPozicija { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public ICollection<Zaposlenici> Zaposlenici { get; set; }
}