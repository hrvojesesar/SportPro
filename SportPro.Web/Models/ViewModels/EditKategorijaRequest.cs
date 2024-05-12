using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditKategorijaRequest
{
    [Key]
    public int IDKategorija { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
}
