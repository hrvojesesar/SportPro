using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddBojaRequest
{
    [Key]
    public int IDBoja { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Naziv boje ne smije imati više od 20 znakova!")]
    public string NazivBoje { get; set; }
}
