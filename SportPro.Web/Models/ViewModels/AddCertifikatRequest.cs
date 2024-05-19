using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddCertifikatRequest
{
    [Key]
    public int IDCertifikat { get; set; }
    public string Naziv { get; set; }
    public string? Opis { get; set; }
    public DateTime DatumDodjele { get; set; }
    public string Organizacija { get; set; }
    public string Slika { get; set; }
}
