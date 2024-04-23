using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditZaposlenikRequest
{
    [Key]
    public int IDZaposlenik { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string Spol { get; set; }
    public string Adresa { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public DateTime DatumZaposlenja { get; set; }
    public string Pozicija { get; set; }
    public string? Cerfitikati { get; set; }
    public double Placa { get; set; }
    public bool Status { get; set; }
}
