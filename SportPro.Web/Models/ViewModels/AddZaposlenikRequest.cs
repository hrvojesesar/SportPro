using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportPro.Web.Models.ViewModels;
public class AddZaposlenikRequest
{
    [Key]
    public int IDZaposlenik { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "Ime ne može imati više od 20 znakova!")]
    public string Ime { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "Prezime ne može imati više od 20 znakova!")]
    public string Prezime { get; set; }
    [Required]
    public char Spol { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "Adresa ne može imati više od 50 znakova!")]
    public string Adresa { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "Grad ne može imati više od 50 znakova!")]
    public string Grad { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "Država ne može imati više od 50 znakova!")]
    public string Drzava { get; set; }
    [Required]
    [RegularExpression(@"^[0-9+]*$", ErrorMessage = "Telefon podržava samo brojeve i znak +!")]
    public string Telefon { get; set; }
    [MaxLength(50, ErrorMessage = "Email ne može imati više od 50 znakova!")]
    public string? Email { get; set; }
    [Required]
    public DateTime DatumZaposlenja { get; set; }
    [Required]
    public double Placa { get; set; }
    [Required]
    public double TopliObrok { get; set; }
    [Required]
    public double Prijevoz { get; set; }
    public double? Bonus { get; set; } = 0;
    public double UkupnaPlaca { get; set; } = 0;
    public string? Certifikati { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "JMBG ne može imati više od 20 znakova!")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "JMBG podržava samo brojeve!")]
    public string JMBG { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "Broj bankovnog računa ne može imati više od 20 znakova!")]
    public string BrojBankovnogRacuna { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "Kvalifikacija ne može imati više od 50 znakova!")]
    public string Kvalifikacija { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Status ne može imati više od 10 znakova!")]
    public string Status { get; set; }
    public DateTime? DatumZavrsetkaRadnogOdnosa { get; set; }
    public int? PoslovnicaID { get; set; }
    public IEnumerable<Poslovnice>? Poslovnices { get; set; }
    public IEnumerable<SelectListItem>? Pozicije { get; set; }
    public string[] SelectedPozicije { get; set; } = Array.Empty<string>();

}
