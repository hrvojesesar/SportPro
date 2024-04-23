using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPosloviRepository
{
    Task<IEnumerable<Poslovi>> GetAllAsync();
    Task<Poslovi> AddAsync(Poslovi poslovi);
    Task<Poslovi?> GetAsync(int? id);
    Task<Poslovi?> UpdateAsync(Poslovi poslovi);
    Task<Poslovi?> DeleteAsync(int id);

}
