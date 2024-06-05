using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IArtikliRepository
{
    Task<IEnumerable<Artikli>> GetAllAsync(int pageNumber = 2, int pageSize = 100);
    Task<Artikli> AddAsync(Artikli artikal);
    Task<Artikli>? GetAsync(int? id);
    Task<Artikli>? UpdateAsync(Artikli artikal);
    Task<Artikli>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
