using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPravilniciRepository
{
    Task<IEnumerable<Pravilnici>> GetAllAsync(string? searchQuery = null, string? searchQuery2 = null, DateTime? startDate = null, DateTime? endDate = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Pravilnici> AddAsync(Pravilnici pravilnik);
    Task<Pravilnici>? GetAsync(int? id);
    Task<Pravilnici>? UpdateAsync(Pravilnici pravilnik);
    Task<Pravilnici>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
