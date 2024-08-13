using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportPro.Web.Models.Domains;

public class Prihodi
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
    public KategorijePrihoda KategorijePrihoda { get; set; }

    [NotMapped]
    public string KategorijaPrihodaNaziv { get; set; }
}
