using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IKandidatiRepository
{
    Task<IEnumerable<Kandidati>> GetAllAsync(string? ime = null, string? prezime = null, string? grad = null, string? natjecaj = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 5, int pageSize = 100);
    Task<Kandidati> AddAsync(Kandidati kandidat);
    Task<Kandidati>? GetAsync(int? id);
    Task<Kandidati>? UpdateAsync(Kandidati kandidat);
    Task<Kandidati>? DeleteAsync(int? id);
    Task<IEnumerable<string>> GetByNatjecajAsync(int? idNatjecaj);
    Task<int> CountAsync();
}
