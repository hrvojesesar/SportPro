using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditTrosakRequest
{
    [Key]
    public int IDTrosak { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public DateTime Datum { get; set; }
    public double Iznos { get; set; }
    public int? Mjesec { get; set; }
    public int? Godina { get; set; }
    public int KategorijeTroskovaIDKategorijaTroska { get; set; }
    public IEnumerable<KategorijeTroskova>? KategorijeTroskovas { get; set; }
}
