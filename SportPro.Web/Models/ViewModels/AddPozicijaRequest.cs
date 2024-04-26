using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;
public class AddPozicijaRequest
{
    [Key]
    public int IDPozicija { get; set; }
    [Required]
    public string Naziv { get; set; }
    public string? Opis { get; set; }
}