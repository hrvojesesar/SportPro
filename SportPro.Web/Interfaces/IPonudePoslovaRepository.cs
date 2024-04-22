using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPonudePoslovaRepository
{
    Task<IEnumerable<PonudePoslova>> GetAllAsync();
    Task<PonudePoslova> AddAsync(PonudePoslova ponudaPoslova);
    Task<PonudePoslova?> GetAsync(int? id);
    Task<PonudePoslova?> UpdateAsync(PonudePoslova ponudaPoslova);
    Task<PonudePoslova?> DeleteAsync(int id);
}
