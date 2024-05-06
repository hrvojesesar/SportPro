using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddTipPromocijeRequest
{
    [Key]
    public int IDTipPromocije { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Naziv ne može imati više od 50 znakova!")]
    public string Naziv { get; set; }
    public string? Opis { get; set; }
}
