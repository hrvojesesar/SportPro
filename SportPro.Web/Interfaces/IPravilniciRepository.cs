using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPravilniciRepository
{
    Task<IEnumerable<Pravilnici>> GetAllAsync();
    Task<Pravilnici> AddAsync(Pravilnici pravilnik);
    Task<Pravilnici>? GetAsync(int? id);
    Task<Pravilnici>? UpdateAsync(Pravilnici pravilnik);
    Task<Pravilnici>? DeleteAsync(int? id);
}
