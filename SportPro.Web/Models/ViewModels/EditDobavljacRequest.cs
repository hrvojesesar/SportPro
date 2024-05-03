using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditDobavljacRequest
{
    [Key]
    public int IDDobavljac { get; set; }
    public string Naziv { get; set; }
    public string Adresa { get; set; }
    public string Grad { get; set; }
    public string Drzava { get; set; }
    public string Telefon { get; set; }
    public string? Email { get; set; }
    public DateTime? PocetakSuradnje { get; set; }
    public DateTime? KrajSuradnje { get; set; }
    public string? SuradnjaAktivna { get; set; }
}
