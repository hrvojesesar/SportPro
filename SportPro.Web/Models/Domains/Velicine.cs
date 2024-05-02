using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.Domains;

public class Velicine
{
    [Key]
    public int IDVelicina { get; set; }
    public string Velicina { get; set; }
}