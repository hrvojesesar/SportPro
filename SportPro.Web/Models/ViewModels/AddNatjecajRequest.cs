using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddNatjecajRequest
{
    [Key]
    public int IDNatjecaj { get; set; }
    [Required]
    [MaxLength(200, ErrorMessage = "Naziv has to be maximum 200 characters!")]
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
    [MaxLength(50, ErrorMessage = "Dobitnik has to be maximum 50 characters!")]
    public string? Dobitnik { get; set; }
}
