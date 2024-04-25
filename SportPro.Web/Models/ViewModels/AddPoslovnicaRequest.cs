using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddPoslovnicaRequest
{
    [Key]
    public int IDPoslovnica { get; set; }
    [Required]
    public string Naziv { get; set; }
    [Required]
    public string Adresa { get; set; }
    [Required]
    public string Grad { get; set; }
    [Required]
    public string Drzava { get; set; }
    [Required]
    public string Telefon { get; set; }
    [Required]
    public string Email { get; set; }
    public TimeSpan? RadnoVrijemeOd { get; set; }
    public TimeSpan? RadnoVrijemeDo { get; set; }
    public DateTime? DatumOtvaranja { get; set; }
    [Required]
    public string Status { get; set; }
}