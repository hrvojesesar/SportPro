using Microsoft.AspNetCore.Mvc.Rendering;
using SportPro.Web.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddArtiklRequest
{
    [Key]
    public int IDArtikal { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Naziv artikla ne može imati više od 100 znakova!")]
    public string Naziv { get; set; }
    [Required]
    public string Opis { get; set; }
    [Required]
    public double Cijena { get; set; }
    [Required]
    public bool Snizen { get; set; }
    public double SnizenaCijena { get; set; } = 0;
    [Required]
    public int NabavnaKolicina { get; set; }
    [Required]
    public int TrenutnaKolicina { get; set; }
    public bool NaStanju { get; set; }
    [Required]
    public DateTime DatumNabave { get; set; }
    [Required]
    public double NabavnaCijena { get; set; }
    [Required]
    public double CijenaDostave { get; set; }
    public double UkupniTrosak { get; set; } = 0;
    public double? UkupnaZarada { get; set; } = 0;
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
