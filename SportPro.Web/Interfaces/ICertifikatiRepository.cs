using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface ICertifikatiRepository
{
    Task<IEnumerable<Certifikati>> GetAllAsync(int pageNumber = 3, int pageSize = 100);
    Task<Certifikati> AddAsync(Certifikati certifikat);
    Task<Certifikati>? GetAsync(int? id);
    Task<Certifikati>? UpdateAsync(Certifikati certifikat);
    Task<int> CountAsync();
}
