using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddKandidatRequest
{
    [Key]
    public int IDKandidat { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Ime ne može imati više od 20 znakova!")]
    public string Ime { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Prezime ne može imati više od 20 znakova!")]
    public string Prezime { get; set; }
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
    [Range(10000, 99999, ErrorMessage = "Poštanski broj mora biti između 10000 i 99999!")]
    public int PostanskiBroj { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Email ne može imati više od 50 znakova!")]
    public string Email { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Telefon ne može imati više od 20 znakova!")]
    public string Telefon { get; set; }
    [Required]
    public DateTime DatumPrijave { get; set; }
    public string? Napomene { get; set; }
    public IEnumerable<SelectListItem>? Natjecaji { get; set; }
    public string[] SelectedNatjecaji { get; set; } = Array.Empty<string>();
}
