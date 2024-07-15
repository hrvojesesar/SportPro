using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface ICertifikatiRepository
{
    Task<IEnumerable<Certifikati>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Certifikati> AddAsync(Certifikati certifikat);
    Task<Certifikati>? GetAsync(int? id);
    Task<Certifikati>? UpdateAsync(Certifikati certifikat);
    Task<int> CountAsync();
}
