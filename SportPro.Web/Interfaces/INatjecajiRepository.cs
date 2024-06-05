using SportPro.Web.Models.Domains;
using Syncfusion.EJ2.TreeGrid;

namespace SportPro.Web.Interfaces;

public interface INatjecajiRepository
{
    Task<IEnumerable<Natjecaji>> GetAllAsync(int pageNumber = 5, int pageSize = 100);
    Task<Natjecaji> AddAsync(Natjecaji natjecaj);
    Task<Natjecaji?> GetAsync(int? id);
    Task<Natjecaji?> UpdateAsync(Natjecaji natjecaj);
    Task<Natjecaji?> DeleteAsync(int id);
    Task<int> CountAsync();
}
