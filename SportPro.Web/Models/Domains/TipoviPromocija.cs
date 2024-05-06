using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class TipoviPromocija
{
    [Key]
    public int IDTipPromocije { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public ICollection<Promocije> Promocije { get; set; }
}