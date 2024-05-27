using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditNarudzbaRequest
{
    [Key]
    public int IDNarudzba { get; set; }
    public string NazivArtikla { get; set; }
    public DateTime DatumNabave { get; set; }
    public DateTime DatumIsporuke { get; set; }
    public int Kolicina { get; set; }
    public double CijenaPoKomadu { get; set; }
    public double CijenaDostave { get; set; }
    public double? Porez { get; set; }
    public double? DodatneNaknade { get; set; }
    public double UkupniTrosak { get; set; }
    public string Status { get; set; }
    public string? Napomene { get; set; }
    public int DobavljacIDDobavljac { get; set; }
    public IEnumerable<Dobavljaci>? Dobavljacis { get; set; }
}
