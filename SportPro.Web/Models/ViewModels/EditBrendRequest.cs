using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditBrendRequest
{
    [Key]
    public int IDBrend { get; set; }
    public string NazivBrenda { get; set; }
    public string Vrsta { get; set; }
    public string Osnivac { get; set; }
    public int GodinaOsnivanja { get; set; }
    public string Sjediste { get; set; }
    public string Predsjednik { get; set; }
    public string Status { get; set; }
}
