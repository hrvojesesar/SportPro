using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddCertifikatRequest
{
    [Key]
    public int IDCertifikat { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Naziv ne može imati više od 100 znakova!")]
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    [Required]
    public DateTime DatumDodjele { get; set; }
    [Required]
    public string Organizacija { get; set; }
    public string Slika { get; set; }
}
