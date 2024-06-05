using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPravilniciRepository
{
    Task<IEnumerable<Pravilnici>> GetAllAsync(int pageNumber = 8, int pageSize = 100);
    Task<Pravilnici> AddAsync(Pravilnici pravilnik);
    Task<Pravilnici>? GetAsync(int? id);
    Task<Pravilnici>? UpdateAsync(Pravilnici pravilnik);
    Task<Pravilnici>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
