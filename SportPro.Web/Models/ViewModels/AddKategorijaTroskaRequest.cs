using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddKategorijaTroskaRequest
{
    [Key]
    public int IDKategorijaTroska { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Naziv ne može imati više od 100 znakova!")]
    public string Naziv { get; set; }
    [StringLength(200, ErrorMessage = "Opis ne može imati više od 200 znakova!")]
    public string? Opis { get; set; }
}
