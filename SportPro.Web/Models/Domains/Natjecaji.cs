using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;
public class Natjecaji
{
    [Key]
    public int IDNatjecaj { get; set; }
    public string Naziv { get; set; }
    public string Opis { get; set; }
    public int ProcijenjenaVrijednost { get; set; }
    public DateTime TrajanjeOd { get; set; }
    public DateTime TrajanjeDo { get; set; }
    public DateTime? DatumObjave { get; set; }
    public bool Aktivan { get; set; }
    [StringLength(int.MaxValue)]
    public string? Dobitnik { get; set; }
    public ICollection<Kandidati> Kandidati { get; set; }
}