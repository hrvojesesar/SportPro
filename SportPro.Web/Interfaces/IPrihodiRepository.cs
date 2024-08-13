using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPrihodiRepository
{
    Task<IEnumerable<Prihodi>> GetAllAsync(string? naziv = null, string? kategorijaPrihoda = null, int? minValue = null, int? maxValue = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Prihodi> AddAsync(Prihodi prihodi);
    Task<Prihodi>? GetAsync(int? id);
    Task<Prihodi>? UpdateAsync(Prihodi prihodi);
    Task<Prihodi>? DeleteAsync(int? id);
    Task<int> CountAsync();

}
