using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPozicijeRepository
{
    Task<IEnumerable<Pozicije>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Pozicije> AddAsync(Pozicije pozicija);
    Task<Pozicije>? GetAsync(int? id);
    Task<Pozicije>? UpdateAsync(Pozicije pozicija);
    Task<Pozicije>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
