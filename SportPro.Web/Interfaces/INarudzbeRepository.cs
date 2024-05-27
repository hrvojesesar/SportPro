using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface INarudzbeRepository
{
    Task<IEnumerable<Narudzbe>> GetAllAsync();
    Task<Narudzbe> AddAsync(Narudzbe narudzba);
    Task<Narudzbe>? GetAsync(int? id);
    Task<Narudzbe>? UpdateAsync(Narudzbe narudzba);
    Task<Narudzbe>? DeleteAsync(int? id);
}
