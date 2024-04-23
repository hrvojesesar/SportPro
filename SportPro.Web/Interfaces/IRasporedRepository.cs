using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IRasporedRepository
{
    Task<IEnumerable<Raspored>> GetAllAsync();
    Task<Raspored> AddAsync(Raspored raspored);
    Task<Raspored?> GetAsync(int? id);
    Task<Raspored?> UpdateAsync(Raspored raspored);
    Task<Raspored?> DeleteAsync(int id);


}
