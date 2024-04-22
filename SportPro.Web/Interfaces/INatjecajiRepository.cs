using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface INatjecajiRepository
{
    Task<IEnumerable<Natjecaji>> GetAllAsync();
    Task<Natjecaji> AddAsync(Natjecaji natjecaj);
    Task<Natjecaji?> GetAsync(int? id);
    Task<Natjecaji?> UpdateAsync(Natjecaji natjecaj);
    Task<Natjecaji?> DeleteAsync(int? id);
    
   
}
