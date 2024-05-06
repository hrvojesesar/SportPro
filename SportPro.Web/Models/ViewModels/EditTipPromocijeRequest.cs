using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditTipPromocijeRequest
{
    [Key]
    public int IDTipPromocije { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
}
