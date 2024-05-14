using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IArtikliRepository
{
    Task<IEnumerable<Artikli>> GetAllAsync();
    Task<Artikli> AddAsync(Artikli artikal);
    Task<Artikli>? GetAsync(int? id);
    Task<Artikli>? UpdateAsync(Artikli artikal);
    Task<Artikli>? DeleteAsync(int? id);
}
