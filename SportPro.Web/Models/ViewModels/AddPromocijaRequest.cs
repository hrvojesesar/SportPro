using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddPromocijaRequest
{
    [Key]
    public int IDPromocije { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Naziv ne može imati više od 50 znakova!")]
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    [Required]
    public DateTime DatumPocetka { get; set; }
    [Required]
    public DateTime DatumZavrsetka { get; set; }
    public bool Aktivna { get; set; }
    public string? DodatniUvjeti { get; set; }
    public string? Slika { get; set; }
    [Required]
    public int TipoviPromocijaIDTipPromocije { get; set; }
    public IEnumerable<TipoviPromocija>? TipoviPromocijas { get; set; }
}
