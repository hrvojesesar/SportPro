using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface ITipoviPromocijaRepository
{
    Task<IEnumerable<TipoviPromocija>> GetAllAsync(int pageNumber = 10, int pageSize = 100);
    Task<TipoviPromocija> AddAsync(TipoviPromocija tipPromocije);
    Task<TipoviPromocija>? GetAsync(int? id);
    Task<TipoviPromocija>? UpdateAsync(TipoviPromocija tipPromocije);
    Task<TipoviPromocija>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
