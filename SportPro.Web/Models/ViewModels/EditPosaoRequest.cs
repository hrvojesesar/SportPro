using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditPosaoRequest
{

    [Key]
    public int IDPosla { get; set; }
    public string Naziv { get; set; }
    public string OpisPosla { get; set; }
    public int BrojOsoba { get; set; }
    public double BrojSati { get; set; }
    public double CijenaSata { get; set; }
    public string? PotrebnaOprema { get; set; }
    public DateTime PocetakRadova { get; set; }
    public DateTime KrajRadova { get; set; }
    public double Trosak { get; set; }
    public double Profit { get; set; }
}
