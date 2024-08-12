using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class KategorijeTroskova
{
    [Key]
    public int IDKategorijaTroska { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public ICollection<Troskovi> Troskovi { get; set; }
}
