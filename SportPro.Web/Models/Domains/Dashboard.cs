using Microsoft.EntityFrameworkCore;

namespace SportPro.Web.Models.Domains;

[Keyless]
public class Dashboard
{
    public IEnumerable<Narudzbe>? Narudzbe { get; set; }
    public IEnumerable<Zaposlenici>? Zaposlenici { get; set; }
    public IEnumerable<Artikli>? Artikli { get; set; }
}
