using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IKategorijePrihodaRepository
{
    Task<IEnumerable<KategorijePrihoda>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 5, int pageSize = 100);
    Task<KategorijePrihoda> AddAsync(KategorijePrihoda kategorijaPrihoda);
    Task<KategorijePrihoda>? GetAsync(int? id);
    Task<KategorijePrihoda>? UpdateAsync(KategorijePrihoda kategorijaPrihoda);
    Task<KategorijePrihoda>? DeleteAsync(int? id);
    Task<int> CountAsync();
    Task<IEnumerable<KategorijePrihoda>> GetAllSecAsync();
}
