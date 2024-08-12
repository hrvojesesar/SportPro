using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditKategorijaTroskaRequest
{
    [Key]
    public int IDKategorijaTroska { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
}
