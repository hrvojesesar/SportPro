using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddTrosakRequest
{
    [Key]
    public int IDTrosak { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Naziv ne može imati više od 100 znakova")]
    public string Naziv { get; set; }
    [StringLength(200, ErrorMessage = "Opis ne može imati više od 200 znakova")]
    public string? Opis { get; set; }
    [Required]
    public DateTime Datum { get; set; }
    [Required]
    public double Iznos { get; set; }
    public int? Mjesec { get; set; }
    public int? Godina { get; set; }
    [Required]
    public int KategorijeTroskovaIDKategorijaTroska { get; set; }
    public IEnumerable<KategorijeTroskova>? KategorijeTroskovas { get; set; }

}
