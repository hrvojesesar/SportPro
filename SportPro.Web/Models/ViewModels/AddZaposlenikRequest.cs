using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddZaposlenikRequest
{
    [Key]
    public int IDZaposlenik { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "Ime has to be maximum 20 characters!")]
    public string Ime { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "Prezime has to be maximum 20 characters!")]
    public string Prezime { get; set; }
    [Required]
    [MaxLength(1, ErrorMessage = "Spol has to be maximum 1 characters!")]
    public string Spol { get; set; }
    [Required]
    [MaxLength(200, ErrorMessage = "Adresa has to be maximum 200 characters!")]
    public string Adresa { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "Telefon has to be maximum 20 characters!")]
    public string Telefon { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Email has to be maximum 100 characters!")]
    public string Email { get; set; }
    [Required]
    public DateTime DatumZaposlenja { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "Pozicija has to be maximum 50 characters!")]
    public string Pozicija { get; set; }
    [MaxLength(200, ErrorMessage = "Cerfitikati has to be maximum 200 characters!")]
    public string? Cerfitikati { get; set; }
    [Required]
    public double Placa { get; set; }
    [Required]
    public bool Status { get; set; }
}
