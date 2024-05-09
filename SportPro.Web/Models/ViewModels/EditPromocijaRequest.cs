using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditPromocijaRequest
{
    [Key]
    public int IDPromocije { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public DateTime DatumPocetka { get; set; }
    public DateTime DatumZavrsetka { get; set; }
    public bool Aktivna { get; set; }
    public string? DodatniUvjeti { get; set; }
    public string? Slika { get; set; }
    public int TipoviPromocijaIDTipPromocije { get; set; }
    public IEnumerable<TipoviPromocija>? TipoviPromocijas { get; set; }
}
