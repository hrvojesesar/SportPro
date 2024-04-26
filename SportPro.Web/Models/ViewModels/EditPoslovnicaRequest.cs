using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;
public class EditPoslovnicaRequest
{
    [Key]
    public int IDPoslovnica { get; set; }
    public string Naziv { get; set; }
    public string Adresa { get; set; }
    public string Grad { get; set; }
    public string Drzava { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public TimeSpan? RadnoVrijemeOd { get; set; }
    public TimeSpan? RadnoVrijemeDo { get; set; }
    public DateTime? DatumOtvaranja { get; set; }
    public string Status { get; set; }
}