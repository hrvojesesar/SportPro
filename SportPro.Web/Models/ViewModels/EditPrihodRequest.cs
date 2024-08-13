using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditPrihodRequest
{
    [Key]
    public int IDPrihod { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public DateTime Datum { get; set; }
    public double Iznos { get; set; }
    public int? Mjesec { get; set; }
    public int? Godina { get; set; }
    public int KategorijePrihodaIDKategorijePrihoda { get; set; }
    public IEnumerable<KategorijePrihoda>? KategorijePrihodas { get; set; }
}
