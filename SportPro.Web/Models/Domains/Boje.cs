using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class Boje
{
    [Key]
    public int IDBoja { get; set; }
    public string NazivBoje { get; set; }
    public ICollection<Artikli> Artikli { get; set; }
}