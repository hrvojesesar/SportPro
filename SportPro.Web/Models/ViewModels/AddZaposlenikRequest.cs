using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportPro.Web.Models.ViewModels;

public class AddZaposlenikRequest
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
    public string? Certifikati { get; set; }
    public string JMBG { get; set; }
    public string BrojBankovnogRacuna { get; set; }
    public string Kvalifikacija { get; set; }
    public DateTime? DatumZavrsetkaRadnogOdnosa { get; set; }
    public int? PoslovnicaID { get; set; }
    public IEnumerable<Poslovnice>? Poslovnices { get; set; }
}
