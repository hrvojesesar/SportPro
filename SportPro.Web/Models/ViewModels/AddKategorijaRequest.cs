using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddKategorijaRequest
{
    [Key]
    public int IDKategorija { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Naziv kategorije ne može imati više od 50 znakova!")]
    public string Naziv { get; set; }
    public string? Opis { get; set; }
}
