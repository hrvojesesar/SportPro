using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddPravilnikRequest
{
    [Key]
    public int IDPravilnik { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Naziv pravilnika ne smije imati više od 100 znakova!")]
    public string Naziv { get; set; }
    [Required]
    public string Opis { get; set; }
    public DateTime? DatumObjavljivanja { get; set; }
    [Required]
    public bool Aktivan { get; set; }
}
