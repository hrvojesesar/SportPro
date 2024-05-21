using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class Kandidati
{
    [Key]
    public int IDKandidat { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string Adresa { get; set; }
    public string Grad { get; set; }
    public string Drzava { get; set; }
    public int PostanskiBroj { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public DateTime DatumPrijave { get; set; }
    public string? Napomene { get; set; }
    public ICollection<Natjecaji> Natjecaji { get; set; }
}
