using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface ITroskoviRepository
{
    Task<IEnumerable<Troskovi>> GetAllAsync();
    Task<Troskovi> AddAsync(Troskovi troskovi);
    Task<Troskovi>? GetAsync(int? id);
    Task<Troskovi>? UpdateAsync(Troskovi troskovi);
    Task<Troskovi>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
