using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditBojaRequest
{
    [Key]
    public int IDBoja { get; set; }
    public string NazivBoje { get; set; }
}
