using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddBrendRequest
{
    [Key]
    public int IDBrend { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Naziv brenda ne može imati više od 20 znakova!")]
    public string NazivBrenda { get; set; }
    [Required]
    [StringLength(10, ErrorMessage = "Vrsta brenda ne može imati više od 10 znakova!")]
    public string Vrsta { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Osnivač brenda ne može imati više od 50 znakova!")]
    public string Osnivac { get; set; }
    [Required]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Godina osnivanja mora imati točno 4 znamenke!")]
    public int GodinaOsnivanja { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Sjedište brenda ne može imati više od 100 znakova!")]
    public string Sjediste { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Predsjednik brenda ne može imati više od 50 znakova!")]
    public string Predsjednik { get; set; }
    [Required]
    [StringLength(10, ErrorMessage = "Status brenda ne može imati više od 10 znakova!")]
    public string Status { get; set; }
}