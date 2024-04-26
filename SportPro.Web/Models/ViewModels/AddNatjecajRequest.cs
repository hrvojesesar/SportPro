using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportPro.Web.Models.ViewModels;
public class AddNatjecajRequest
{
    [Key]
    public int IDNatjecaj { get; set; }
    [Required]
    public string Naziv { get; set; }
    [Required]
    public string Opis { get; set; }
    [Required]
    public int ProcijenjenaVrijednost { get; set; }
    [Required]
    public DateTime TrajanjeOd { get; set; }
    [Required]
    public DateTime TrajanjeDo { get; set; }
    public DateTime? DatumObjave { get; set; }
    public bool Aktivan { get; set; }
    public string? Dobitnik { get; set; }
}