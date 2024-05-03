using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditVrstaPlacanjaRequest
{
    [Key]
    public int IDVrstaPlacanja { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public bool Aktivno { get; set; }
}