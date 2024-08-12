using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportPro.Web.Models.Domains;

public class Troskovi
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
    public KategorijeTroskova KategorijeTroskova { get; set; }

    [NotMapped]
    public string KategorijaTroskaNaziv { get; set; }
}
