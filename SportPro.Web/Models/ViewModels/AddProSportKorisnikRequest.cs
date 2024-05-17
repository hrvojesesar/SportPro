using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddProSportKorisnikRequest
{
    [Key]
    public int IDKorisnik { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Ime ne može imati više od 20 znakova!")]
    public string Ime { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Prezime ne može imati više od 20 znakova!")]
    public string Prezime { get; set; }
    [Required]
    public string Spol { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Prezime ne može imati više od 50 znakova!")]
    public string Email { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Lozinka ne može imati više od 20 znakova!")]
    public string Lozinka { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Telefon ne može imati više od 20 znakova!")]
    public string Telefon { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Adresa ne može imati više od 50 znakova!")]
    public string Adresa { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Grad ne može imati više od 50 znakova!")]
    public string Grad { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Država ne može imati više od 50 znakova!")]
    public string Drzava { get; set; }
    [Required]
    [Range(10000, 99999, ErrorMessage = "Poštanski broj mora biti između 10000 i 99999")]
    public int PostanskiBroj { get; set; }
    [Required]
    public DateTime DatumRodenja { get; set; }
    [Required]
    public DateTime DatumRegistracije { get; set; }
    [Required]
    public bool Verificiran { get; set; }
    [Required]
    public string Status { get; set; }
}
