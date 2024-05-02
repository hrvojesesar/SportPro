using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddVelicinaRequest
{
    [Key]
    public int IDVelicina { get; set; }
    [Required]
    [StringLength(5, ErrorMessage = "Veličina ne može imati više od 5 znakova!")]
    public string Velicina { get; set; }
}
