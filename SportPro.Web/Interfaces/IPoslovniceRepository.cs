using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPoslovniceRepository
{
    Task<IEnumerable<Poslovnice>> GetAllAsync(string? naziv = null, string? grad = null, string? status = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Poslovnice> AddAsync(Poslovnice poslovnice);
    Task<Poslovnice>? GetAsync(int? id);
    Task<Poslovnice>? UpdateAsync(Poslovnice poslovnice);
    Task<Poslovnice>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
