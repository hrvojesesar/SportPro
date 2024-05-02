using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IBojeRepository
{
    Task<IEnumerable<Boje>> GetAllAsync();
    Task<Boje> AddAsync(Boje boja);
    Task<Boje>? GetAsync(int? id);
    Task<Boje>? UpdateAsync(Boje boja);
}
