using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPromocijeRepository
{
    Task<IEnumerable<Promocije>> GetAllAsync(int pageNumber = 3, int pageSize = 100);
    Task<Promocije> AddAsync(Promocije promocije);
    Task<Promocije>? GetAsync(int? id);
    Task<Promocije>? UpdateAsync(Promocije promocije);
    Task<Promocije>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
