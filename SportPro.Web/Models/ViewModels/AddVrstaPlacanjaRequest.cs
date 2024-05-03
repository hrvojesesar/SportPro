using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddVrstaPlacanjaRequest
{
    [Key]
    public int IDVrstaPlacanja { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Naziv vrste plaćanja ne može imati više od 20 znakova!")]
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public bool Aktivno { get; set; }
}