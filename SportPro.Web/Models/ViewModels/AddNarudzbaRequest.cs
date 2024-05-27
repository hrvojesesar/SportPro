using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddNarudzbaRequest
{
    [Key]
    public int IDNarudzba { get; set; }
    [Required]
    public string NazivArtikla { get; set; }
    [Required]
    public DateTime DatumNabave { get; set; }
    [Required]
    public DateTime DatumIsporuke { get; set; }
    [Required]
    public int Kolicina { get; set; }
    [Required]
    public double CijenaPoKomadu { get; set; }
    [Required]
    public double CijenaDostave { get; set; }
    public double? Porez { get; set; }
    public double? DodatneNaknade { get; set; }
    public double UkupniTrosak { get; set; } = 0;
    [Required]
    public string Status { get; set; }
    public string? Napomene { get; set; }
    public int DobavljacIDDobavljac { get; set; }
    public IEnumerable<Dobavljaci> Dobavljacis { get; set; }
}
