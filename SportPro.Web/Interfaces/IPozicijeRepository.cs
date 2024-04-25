using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPozicijeRepository
{
    Task<IEnumerable<Pozicije>> GetAllAsync();
    Task<Pozicije> AddAsync(Pozicije pozicija);
    Task<Pozicije>? GetAsync(int? id);
    Task<Pozicije>? UpdateAsync(Pozicije pozicija);
    Task<Pozicije>? DeleteAsync(int? id);
}
