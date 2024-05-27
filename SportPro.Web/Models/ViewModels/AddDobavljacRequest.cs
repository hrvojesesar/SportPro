using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddDobavljacRequest
{
    [Key]
    public int IDDobavljac { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Naziv dobavljača ne smije imati više od 20 znakova!")]
    public string Naziv { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Adresa dobavljača ne smije imati više od 100 znakova!")]
    public string Adresa { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Grad dobavljača ne smije imati više od 50 znakova!")]
    public string Grad { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Država dobavljača ne smije imati više od 50 znakova!")]
    public string Drzava { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Telefon dobavljača ne smije imati više od 20 znakova!")]
    public string Telefon { get; set; }
    [StringLength(100, ErrorMessage = "Email dobavljača ne smije imati više od 100 znakova!")]
    public string? Email { get; set; }
    public DateTime? PocetakSuradnje { get; set; }
    //[Compare(nameof(PocetakSuradnje), ErrorMessage = "Datum početka suradnje ne može biti veći od datuma završetka suradnje!")]
    public DateTime? KrajSuradnje { get; set; }
    public string? SuradnjaAktivna { get; set; }
}
