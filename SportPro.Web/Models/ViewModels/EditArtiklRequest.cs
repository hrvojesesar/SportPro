using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditArtiklRequest
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
    public int DobavljacIDDobavljac { get; set; }
    public int BrendIDBrend { get; set; }
    public IEnumerable<Dobavljaci> Dobavljacis { get; set; }
    public IEnumerable<Brendovi> Brendovis { get; set; }
    public IEnumerable<SelectListItem>? Kategorije { get; set; }
    public string[] SelectedKategorije { get; set; } = Array.Empty<string>();
    public IEnumerable<SelectListItem>? Boje { get; set; }
    public string[] SelectedBoje { get; set; } = Array.Empty<string>();
    public IEnumerable<SelectListItem>? Velicine { get; set; }
    public string[] SelectedVelicine { get; set; } = Array.Empty<string>();
    public IEnumerable<SelectListItem> Poslovnice { get; set; }
    public string[] SelectedPoslovnice { get; set; } = Array.Empty<string>();
}
