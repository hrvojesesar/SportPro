using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddPosaoRequest
{
    [Key]
    public int IDPosla { get; set; }
    [Required]
    [MaxLength(200, ErrorMessage = "Naziv has to be maximum 50 characters!")]
    public string Naziv { get; set; }
    [Required]
    public string OpisPosla { get; set; }
    [Required]
    public int BrojOsoba { get; set; }
    [Required]
    public double BrojSati { get; set; }
    [Required]
    public double CijenaSata { get; set; }
    [MaxLength(200, ErrorMessage = "PotrebnaOprema has to be maximum 200 characters!")]
    public string? PotrebnaOprema { get; set; }
    [Required]
    public DateTime PocetakRadova { get; set; }
    [Required]
    public DateTime KrajRadova { get; set; }
    [Required]
    public double Trosak { get; set; }
    [Required]
    public double Profit { get; set; }
}
