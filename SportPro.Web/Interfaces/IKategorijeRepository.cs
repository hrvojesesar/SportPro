using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IKategorijeRepository
{
    Task<IEnumerable<Kategorije>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Kategorije> AddAsync(Kategorije kategorija);
    Task<Kategorije>? GetAsync(int? id);
    Task<Kategorije>? UpdateAsync(Kategorije kategorija);
    Task<Kategorije>? DeleteAsync(int? id);
    Task<int> CountAsync();

}
