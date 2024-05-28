using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;
public class Zaposlenici
{
    [Key]
    public int IDZaposlenik { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public char Spol { get; set; }
    public string Adresa { get; set; }
    public string Grad { get; set; }
    public string Drzava { get; set; }
    public string Telefon { get; set; }
    public string? Email { get; set; }
    public DateTime DatumZaposlenja { get; set; }
    public double Placa { get; set; }
    public double TopliObrok { get; set; }
    public double Prijevoz { get; set; }
    public double? Bonus { get; set; }
    public double UkupnaPlaca { get; set; }
    public string? Certifikati { get; set; }
    public string JMBG { get; set; }
    public string BrojBankovnogRacuna { get; set; }
    public string Kvalifikacija { get; set; }
    public string Status { get; set; }
    public DateTime? DatumZavrsetkaRadnogOdnosa { get; set; }
    public int? PoslovnicaID { get; set; }
    public ICollection<Pozicije> Pozicije { get; set; }
    public Poslovnice Poslovnica { get; set; }
}