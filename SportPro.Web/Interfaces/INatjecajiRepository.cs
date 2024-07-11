using SportPro.Web.Models.Domains;
using Syncfusion.EJ2.TreeGrid;

namespace SportPro.Web.Interfaces;

public interface INatjecajiRepository
{
    Task<IEnumerable<Natjecaji>> GetAllAsync(string? searchQuery = null, string? searchQuery2 = null, int? minValue = null, int? maxValue = null, DateTime? startDate = null, DateTime? endDate = null, string? sortBy = null, string? sortDirection = null, int pageNumber= 1, int pageSize = 100);
    Task<Natjecaji> AddAsync(Natjecaji natjecaj);
    Task<Natjecaji?> GetAsync(int? id);
    Task<Natjecaji?> UpdateAsync(Natjecaji natjecaj);
    Task<Natjecaji?> DeleteAsync(int id);
    Task<int> CountAsync();
    Task<IEnumerable<Natjecaji>> GetAllForKandidatiAsync();
}
