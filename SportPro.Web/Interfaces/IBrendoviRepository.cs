using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IBrendoviRepository
{
    Task<IEnumerable<Brendovi>> GetAllAsync(string? nazivBrenda = null, string? vrsta = null, string? status = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Brendovi> AddAsync(Brendovi brend);
    Task<Brendovi>? GetAsync(int? id);
    Task<Brendovi>? UpdateAsync(Brendovi brend);
    Task<Brendovi>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
