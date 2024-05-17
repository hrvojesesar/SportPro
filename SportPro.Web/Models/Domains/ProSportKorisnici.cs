using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class ProSportKorisnici
{
    [Key]
    public int IDKorisnik { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string Spol { get; set; }
    public string Email { get; set; }
    public string Lozinka { get; set; }
    public string Telefon { get; set; }
    public string Adresa { get; set; }
    public string Grad { get; set; }
    public string Drzava { get; set; }
    public int PostanskiBroj { get; set; }
    public DateTime DatumRodenja { get; set; }
    public DateTime DatumRegistracije { get; set; }
    public bool Verificiran { get; set; }
    public string Status { get; set; }
}
