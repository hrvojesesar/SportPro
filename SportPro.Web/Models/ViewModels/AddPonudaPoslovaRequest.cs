using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddPonudaPoslovaRequest
{
    [Key]
    public int IDPonudaPoslova { get; set; }
    [Required]
    public string Naziv { get; set; }
    [Required]
    public string OpisPosla { get; set; }
    [Required]
    public int BrojOsoba { get; set; }
    [Required]
    public double BrojSati { get; set; }
    [Required]
    public double CijenaSata { get; set; }
    public string? PotrebnaOprema { get; set; }
    [Required]
    public DateTime PocetakRadova { get; set; }
    [Required]
    public DateTime KrajRadova { get; set; }
    [Required]
    public string Lokacija { get; set; }
}
