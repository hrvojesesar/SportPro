using Microsoft.EntityFrameworkCore.Query.Internal;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPromocijeRepository
{
    Task<IEnumerable<Promocije>> GetAllAsync(string? naziv = null, string? tipPromocije = null, string? aktivan = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Promocije> AddAsync(Promocije promocije);
    Task<Promocije>? GetAsync(int? id);
    Task<Promocije>? UpdateAsync(Promocije promocije);
    Task<Promocije>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
