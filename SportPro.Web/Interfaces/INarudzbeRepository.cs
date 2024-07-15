using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface INarudzbeRepository
{
    Task<IEnumerable<Narudzbe>> GetAllAsync(string? naziv = null, DateTime? startDate = null, DateTime? endDate = null, string? status = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Narudzbe> AddAsync(Narudzbe narudzba);
    Task<Narudzbe>? GetAsync(int? id);
    Task<Narudzbe>? UpdateAsync(Narudzbe narudzba);
    Task<Narudzbe>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
