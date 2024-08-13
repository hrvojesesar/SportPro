using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditKategorijaPrihodaRequest
{
    [Key]
    public int IDKategorijePrihoda { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
}
