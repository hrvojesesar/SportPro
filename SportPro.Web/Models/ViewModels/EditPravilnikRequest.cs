using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditPravilnikRequest
{
    [Key]
    public int IDPravilnik { get; set; }
    public string Naziv { get; set; }
    public string Opis { get; set; }
    public DateTime? DatumObjavljivanja { get; set; }
    public bool Aktivan { get; set; }
}
