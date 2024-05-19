using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface ICertifikatiRepository
{
    Task<IEnumerable<Certifikati>> GetAllAsync();
    Task<Certifikati> AddAsync(Certifikati certifikat);
    Task<Certifikati>? GetAsync(int? id);
    Task<Certifikati>? UpdateAsync(Certifikati certifikat);
}
