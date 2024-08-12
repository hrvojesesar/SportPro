using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IKategorijeTroskovaRepository
{
    Task<IEnumerable<KategorijeTroskova>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 5, int pageSize = 100);
    Task<KategorijeTroskova> AddAsync(KategorijeTroskova kategorijaTroska);
    Task<KategorijeTroskova>? GetAsync(int? id);
    Task<KategorijeTroskova>? UpdateAsync(KategorijeTroskova kategorijaTroska);
    Task<KategorijeTroskova>? DeleteAsync(int? id);
    Task<int> CountAsync();
    Task<IEnumerable<KategorijeTroskova>> GetAllSecAsync();
}
