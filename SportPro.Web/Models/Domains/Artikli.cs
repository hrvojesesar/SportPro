using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class Artikli
{
    [Key]
    public int IDArtikal { get; set; }
    public string Naziv { get; set; }
    public string Opis { get; set; }
    public double Cijena { get; set; }
    public bool Snizen { get; set; }
    public double SnizenaCijena { get; set; }
    public int NabavnaKolicina { get; set; }
    public int TrenutnaKolicina { get; set; }
    public bool NaStanju { get; set; }
    public DateTime DatumNabave { get; set; }
    public double NabavnaCijena { get; set; }
    public double CijenaDostave { get; set; }
    public double UkupniTrosak { get; set; }
    public double? UkupnaZarada { get; set; } 
    public int DobavljacIDDobavljac { get; set; }
    public int BrendIDBrend { get; set; }
    public ICollection<Kategorije> Kategorije { get; set; }
    public Dobavljaci Dobavljac { get; set; }
    public Brendovi Brend { get; set; }
    public ICollection<Boje> Boje { get; set; }
    public ICollection<Velicine>? Velicine { get; set; }
    public ICollection<Poslovnice> Poslovnice { get; set; }
}
