using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IVelicineRepository
{
    Task<IEnumerable<Velicine>> GetAllAsync();
    Task<Velicine> AddAsync(Velicine velicina);
    Task<Velicine>? GetAsync(int? id);
    Task<Velicine>? UpdateAsync(Velicine velicina);
    Task<Velicine>? DeleteAsync(int? id);
}
