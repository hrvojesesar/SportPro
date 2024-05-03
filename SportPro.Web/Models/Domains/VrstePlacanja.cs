using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class VrstePlacanja
{
    [Key]
    public int IDVrstaPlacanja { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public bool Aktivno { get; set; }
}