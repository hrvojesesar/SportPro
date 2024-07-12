using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface ITipoviPromocijaRepository
{
    Task<IEnumerable<TipoviPromocija>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 5, int pageSize = 100);
    Task<TipoviPromocija> AddAsync(TipoviPromocija tipPromocije);
    Task<TipoviPromocija>? GetAsync(int? id);
    Task<TipoviPromocija>? UpdateAsync(TipoviPromocija tipPromocije);
    Task<TipoviPromocija>? DeleteAsync(int? id);
    Task<int> CountAsync();
    Task<IEnumerable<TipoviPromocija>> GetAllSecAsync();

}
