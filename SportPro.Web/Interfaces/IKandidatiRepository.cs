using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IKandidatiRepository
{
    Task<IEnumerable<Kandidati>> GetAllAsync();
    Task<Kandidati> AddAsync(Kandidati kandidat);
    Task<Kandidati>? GetAsync(int? id);
    Task<Kandidati>? UpdateAsync(Kandidati kandidat);
    Task<Kandidati>? DeleteAsync(int? id);
}
