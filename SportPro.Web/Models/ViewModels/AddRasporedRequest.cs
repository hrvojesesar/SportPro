using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddRasporedRequest
{
    [Key]
    public int IDRaspored { get; set; }
    [Required]
    public string Zaposlenik { get; set; }
    [Required]
    public string Posao { get; set; }
    public string? Napomena { get; set; }
    [Required]
    public DateTime PocetakRada { get; set; }
    [Required]
    public DateTime KrajRada { get; set; }
    [Required]
    public double BrojSati { get; set; }

}
