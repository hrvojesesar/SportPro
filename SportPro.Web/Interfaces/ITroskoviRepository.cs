using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface ITroskoviRepository
{
    Task<IEnumerable<Troskovi>> GetAllAsync(string? naziv = null, string? kategorijaTroska = null, int? minValue = null, int? maxValue = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Troskovi> AddAsync(Troskovi troskovi);
    Task<Troskovi>? GetAsync(int? id);
    Task<Troskovi>? UpdateAsync(Troskovi troskovi);
    Task<Troskovi>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
