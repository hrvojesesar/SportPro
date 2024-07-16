using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IArtikliRepository
{
    Task<IEnumerable<Artikli>> GetAllAsync(string? naziv = null, int? minValue = null, int? maxValue = null, string? snizen = null, string? naStanju = null, string? kategorija = null, string? poslovnica = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 5, int pageSize = 100);
    Task<Artikli> AddAsync(Artikli artikal);
    Task<Artikli>? GetAsync(int? id);
    Task<Artikli>? UpdateAsync(Artikli artikal);
    Task<Artikli>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
