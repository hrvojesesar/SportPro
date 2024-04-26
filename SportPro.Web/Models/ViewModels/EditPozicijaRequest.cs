using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;
public class EditPozicijaRequest
{
    [Key]
    public int IDPozicija { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
}